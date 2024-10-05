using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimsControllers : ControllerBase
    {
        private IUnitOfWork _unit{get;set;}
        public ClaimsControllers(IUnitOfWork unit)
        {
            _unit=unit;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> claimToUser(string email, string ClaimName, string ClaimValue){
            if(ModelState.IsValid){
                return Ok(await _unit.claims.addClaimToUser(email , ClaimName , ClaimValue));
            }
            return BadRequest();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> claimToRole(string RoleName, string ClaimName, string ClaimValue){
            if(ModelState.IsValid){
                return Ok(await _unit.claims.addClaimToRole(RoleName , ClaimName , ClaimValue));
            }
            return BadRequest();
        }
    }
}