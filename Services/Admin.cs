using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Expense_Tracker.Services.Interfaces;

namespace Expense_Tracker.Services
{
    public class Admin : IAdmin
    {
        public RoleManager<IdentityRole> _roleManager { get; set; }
        public UserManager<IdentityUser> _userManager { get; set; }
        public Admin(RoleManager<IdentityRole> roleManager , UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<string> AddRole(string RoleName)
        {
            var role = await _roleManager.FindByNameAsync(RoleName);
            if(role!=null){
                return "The Role is alreay exist";
            }
            var result = _roleManager.CreateAsync(new IdentityRole(RoleName));
            if(result != null){
                return $"The Role {RoleName} just added";
            }
            return "The Role can't to be added";
        }

        public async Task<string> AddRoleToUser(string email , string RoleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null){
                return "The user does not exist";
            }
            var role = await _roleManager.RoleExistsAsync(RoleName);
            if(!role){
                return "The role does not exist";
            }
            var result = await _userManager.AddToRoleAsync(user , RoleName);
            return $"The Role {RoleName} Added To The User {user.UserName}";
        }
    }
}