using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using Expense_Tracker.Services.Interfaces;

namespace Expense_Tracker.Services
{
    public class Claims : IClaims
    {
        public UserManager<IdentityUser> _userManager { get; set; }
        public RoleManager<IdentityRole> _roleManager { get; set; }
        public Claims(UserManager<IdentityUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> addClaimToUser(string email, string ClaimName, string ClaimValue)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null){
                return "The user does not exist";
            }
            var claim = new Claim(ClaimName , ClaimValue);
            var result = _userManager.AddClaimAsync(user , claim);
            if(result != null){
                return $"The claim {ClaimName} is added to the user {user.UserName}";
            }
            return "Can't be added";
        }

        public async Task<string> addClaimToRole(string RoleName, string ClaimName, string ClaimValue)
        {
            var role = await _roleManager.FindByNameAsync(RoleName);
            if(role == null){
                return "The role does not exist";
            }
            var claim = new Claim(ClaimName , ClaimValue);
            var result = _roleManager.AddClaimAsync(role , claim);
            if(result != null){
                return $"The claim {ClaimName} is added to the role {role.Name}";
            }
            return "Can't be added";
        }
    }
}