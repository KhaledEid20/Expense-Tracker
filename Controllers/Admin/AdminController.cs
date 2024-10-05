using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private IUnitOfWork _unit{get;set;}
        public AdminController(IUnitOfWork unit)
        {
            _unit = unit;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> addRole(string RoleName){
            if(ModelState.IsValid){
                return Ok(await _unit.admin.AddRole(RoleName));
            }
            return BadRequest();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RoleToUser(string email ,string RoleName){
            if(ModelState.IsValid){
                return Ok(await _unit.admin.AddRoleToUser(email,RoleName));
            }
            return BadRequest();
        }
    }
}