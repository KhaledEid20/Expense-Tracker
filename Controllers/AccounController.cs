using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccounController : ControllerBase
    {
       private IUnitOfWork _unit{get ; set;}
       public AccounController(IUnitOfWork unit)
       {
            _unit = unit;
       } 
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterUser([FromBody]registerDTO user){
            if(ModelState.IsValid){
                return Ok(await _unit.account.register(user));
            }
            return BadRequest();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginUser([FromBody]LoginDTO user){
            if(ModelState.IsValid){
                return Ok(await _unit.account.login(user));
            }
            return BadRequest();            
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> refreshToken([FromBody] AuthToken user){
            if(ModelState.IsValid){
                return Ok(await _unit.account.Refresh(user));
            }
            return BadRequest();
        }
    }
}