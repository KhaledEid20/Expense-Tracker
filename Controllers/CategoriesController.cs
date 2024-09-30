using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        public  IUnitOfWork _unit {get; set;}
        public CategoriesController(IUnitOfWork unit)
        {
            _unit = unit;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddCategory(CategoryDto category){
            if(ModelState.IsValid){
                return Ok(await _unit.category.AppendCategory(category));
            }
            return BadRequest();
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveCategory(CategoryDto category){
            if(ModelState.IsValid){
                return Ok(await _unit.category.DeleteCategory(category));
            }
            return BadRequest();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> AllCategories(){
            return Ok(await _unit.category.GetAllCategory());
        }
    }
}