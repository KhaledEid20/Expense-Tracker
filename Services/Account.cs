using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Expense_Tracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace Expense_Tracker.Services
{
    public class Account : Base<RefreshToken>,Interfaces.IAccount
    {
        public Account(AppDbContext context , UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration) : base(context , userManager , roleManager ,configuration)
        {

        }
        public async Task<AuthResultDto> register(registerDTO user)
        {
                var appuser = new IdentityUser{
                UserName = user.Name,
                Email = user.Email
            };
            var result = await _userManager.CreateAsync(appuser , user.Password);
            await _userManager.AddToRoleAsync(appuser, "AppUser");
            var Tokens = await GenerateToken(appuser);
            if(result.Succeeded){
                return new AuthResultDto{
                    Token = Tokens.Token,
                    RefreshToken =Tokens.RefreshToken,
                    error = Tokens.error,
                    result = true
                };
            }
            return new AuthResultDto{
                    error =  "The user can't be created",
                    result = false
                };
        }
        public async Task<AuthResultDto> login(LoginDTO user)
        {
            var appuser = await _userManager.FindByEmailAsync(user.Email);
            if(appuser == null){
                return new AuthResultDto{
                    error = "User Does Not Exist",
                    result = false
                };
            }
            var Tokens = await GenerateToken(appuser);
            if(await _userManager.CheckPasswordAsync(appuser , user.Password)){
                return new AuthResultDto{
                    Token = Tokens.Token,
                    RefreshToken = Tokens.RefreshToken,
                    error = Tokens.error,
                    result = Tokens.result
                };
            }
            return new AuthResultDto{
                error = "user Email or password is wrong",
                result = false
            };
        }
        public async Task<AuthResultDto> Refresh(AuthToken authResult)
        {
            var jwtSettings = _configuration.GetSection("jwtSettings");
            var key = jwtSettings["key"];
            var _tokenValidationParameter = new TokenValidationParameters{
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidIssuer = jwtSettings["Issuer"],
            };

            //now we check that the Token Is valid;

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var claimsPrincipal = tokenHandler.ValidateToken(authResult.Token ,
            _tokenValidationParameter,
            out securityToken);
            var JwtSecurityToken = securityToken as JwtSecurityToken;  
            if(JwtSecurityToken == null || !JwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256 , StringComparison.InvariantCultureIgnoreCase)){
                throw new SecurityTokenException("Invalid Token");
            }

             //we just checked if The access Token Is still Valid or not

            var utcExpiryDate = long.Parse(claimsPrincipal.Claims.FirstOrDefault(f => f.Type== JwtRegisteredClaimNames.Exp).Value);
            var ExpiryDate = await DateTimeToDateStamp(utcExpiryDate);
            
            // ExpiryDate = DateTime.Now.AddHours(-1); // just for testing
            if(ExpiryDate > DateTime.Now){
                return new AuthResultDto{
                    error = "the token Is still valid",
                    result = false
                };
            }
            var refreshToken = await _context.refreshTokens.FirstOrDefaultAsync(t => t.refreshToken == authResult.RefreshToke);
            var jti = claimsPrincipal.Claims.FirstOrDefault(j => j.Type == JwtRegisteredClaimNames.Jti).Value;
            if(refreshToken == null){
            return new AuthResultDto{
                    error = "the token Is still valid",
                    result = false
                };
            }

            if(refreshToken.IsRevoked || refreshToken.IsUsed
                || refreshToken.ExpiryDate < DateTime.UtcNow || 
                refreshToken.AccessTokenId != jti) {
                    return new AuthResultDto{
                        error = "can't use this refreshToken",
                        result = false
                    };
            }

            _context.refreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(refreshToken.UserID);

            return await GenerateToken(user);
        }
    }
}