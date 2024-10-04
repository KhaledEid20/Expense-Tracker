using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Data;
namespace Expense_Tracker.Services
{
    public class Base<T> : IBase<T> where T : class
    {
        private AppDbContext context;

        public UserManager<IdentityUser> _userManager { get; set; }
        public RoleManager<IdentityRole> _roleManager { get; set; }
        public IConfiguration _configuration;
        
        public AppDbContext _context {get; set;}
        public Base(AppDbContext context , UserManager<IdentityUser> userManager , RoleManager<IdentityRole> roleManager , IConfiguration configuration){
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<List<CategoryResult>> GetAllCategories()
        {
            var Elements = await _context.Categories.ToListAsync();
            if(Elements == null){
                return new List<CategoryResult>(){
                    new CategoryResult{
                        CategoryName = "No Element Exist",
                        result = false
                    }
                };
            }
            var result = Elements.Select(e => new CategoryResult{
                CategoryName = e.CategoryName,
                result = true

            }).ToList();
            return result;
        }

        public async Task<List<ExpensesResult>> GetAllExpenses()
        {
            var Elements = await _context.Tasks.Include(i => i.category).ToListAsync();
            if(Elements == null){
                return new List<ExpensesResult>(){
                    new ExpensesResult{
                        error = "No Element Exist",
                        result = false
                    }
                };
            }
                    var result = Elements.Select(e => new ExpensesResult{
                    description = e.description,
                    spentMoney = e.spentMoney,
                    CategoryId = e.category.CategoryName,
                    result = true,
                    error = ""
                }).ToList();

            return result;
        }

        #region Token Creation & Claims
        // The Token Creation
        public async Task<AuthResultDto> GenerateToken(IdentityUser appuser)
        {
            if(appuser == null){
                return new AuthResultDto{
                    error = "The User does not exist so can't generate Claims",
                    result = false
                };
            }
            var jwtSettings = _configuration.GetSection("jwtSettings");
            var claims = await GetClaims(appuser);
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["key"]));
            var sc = new SigningCredentials(secret , SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                claims : claims,
                issuer : jwtSettings["Issuer"],
                expires : DateTime.Now.AddMinutes(20),
                signingCredentials : sc
            );
            var RefreshToken = new RefreshToken(){
                refreshToken = await GenerateRefreshToken(30),
                UserID = appuser.Id,
                AccessTokenId = token.Id,
                IsRevoked = false,
                IsUsed = false,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(3)
            };

            await _context.refreshTokens.AddAsync(RefreshToken);
            await _context.SaveChangesAsync();

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            
            return new AuthResultDto{
                Token = jwt,
                RefreshToken = RefreshToken.refreshToken,
                error = "",
                result = true
            };
        }
        // Get Claims
        public async Task<List<Claim>> GetClaims(IdentityUser user)
        {
            // Check if user is null
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            List<Claim> claims = new List<Claim>();

            var iat = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString();
            // Use null-coalescing operator to handle potential nulls
            claims.Add(new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)); // Default to empty if null
            claims.Add(new Claim(ClaimTypes.Email, user.Email ?? string.Empty)); // Default to empty if null
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, iat, ClaimValueTypes.Integer64));

            // Retrieve user claims safely
            var UserClaims = await _userManager.GetClaimsAsync(user);
            if (UserClaims != null && UserClaims.Count > 0)
            {
                claims.AddRange(UserClaims);
            }

            // Retrieve roles and handle potential nulls
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                if (string.IsNullOrWhiteSpace(role))
                {
                    continue; // Skip if the role is null or empty
                }

                claims.Add(new Claim(ClaimTypes.Role, role));

                // Check if the role exists before attempting to retrieve claims
                var Role = await _roleManager.FindByNameAsync(role);
                if (Role != null)
                {
                    var RoleClaims = await _roleManager.GetClaimsAsync(Role);
                    claims.AddRange(RoleClaims);
                }
            }

            return claims;
        }

        #endregion
        
        private async Task<string> GenerateRefreshToken(int length){
            var random = new Random();
            var chars = "ABCDEFGHIJKLMOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789_$";
            return new string(Enumerable.Repeat(chars , length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<DateTime> DateTimeToDateStamp(long time){
            var utc = new DateTime(1970 , 1 , 1 , 0 , 0 ,0 ,DateTimeKind.Utc);
            utc = utc.AddMinutes(20).ToUniversalTime();
            return utc;
        } 
    }
}