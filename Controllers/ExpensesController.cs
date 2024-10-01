using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        public IUnitOfWork _unit {get; set;}
        public ExpensesController(IUnitOfWork unit)
        {
            _unit = unit;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllExpenses(){
            return Ok(await _unit.expenses.GetExpenses());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> pastDay(){
            return Ok(await _unit.expenses.OneDayExpenses());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> pastMounth(){
            return Ok(await _unit.expenses.OneMounthExpenses());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> pastYear(){
            return Ok(await _unit.expenses.OneYearExpenses());
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddExpense(ExpensesDto expense){
            if(ModelState.IsValid){
                return Ok(await _unit.expenses.AddExpenses(expense));
            }
            return BadRequest();
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveExpense(ExpensesDto expense){
            if(ModelState.IsValid){
                return Ok(await _unit.expenses.RemoveExpenses(expense));
            }
            return BadRequest();
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateExpense(ExpensesDto expense){
            if(ModelState.IsValid){
                return Ok(await _unit.expenses.updateExpenses(expense));
            }
            return BadRequest();
        }
    }
}