using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expense_Tracker.Data;
using Expense_Tracker.Services.Interfaces;

namespace Expense_Tracker.Services
{
    public class ExpensesRepo : Base<Expenses>,IExpenses
    {
        public ExpensesRepo(AppDbContext context , UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration) : base(context , userManager , roleManager ,configuration)
        {
        }
        
        public async Task<ExpensesResult> AddExpenses(ExpensesDto expense)
        {
            var exp = await _context.Tasks.FirstOrDefaultAsync(x => x.description == expense.description);
            if(exp == null){
                var ex = new Expenses(){
                    description = expense.description,
                    spentMoney = expense.spentMoney,
                    CategoryId = expense.CategoryId
                };
                await _context.Tasks.AddAsync(ex);
                await _context.SaveChangesAsync();
                var category = await _context.Categories.FirstOrDefaultAsync(x=> x.Id == ex.CategoryId);
                return new ExpensesResult{
                    description = ex.description,
                    CategoryId = category.CategoryName,
                    spentMoney = ex.spentMoney,
                    Date = ex.Date,
                    result = true
                };
            }
            else{
                return new ExpensesResult{
                    error = $"The expense {expense.description} already exist",
                    result = false
                };
            }
        }

        public async Task<List<ExpensesResult>> GetExpenses()
        {
            return await GetAllExpenses();
        }

        public async Task<List<ExpensesResult>> OneDayExpenses()
        {
            var oneDay = DateTime.UtcNow.AddDays(-1).Date;
            var expenses = await _context.Tasks
            .Where(x => x.Date.Date == oneDay)
            .Include(x=>x.category).ToListAsync();
            if(expenses == null || !expenses.Any()){
                return new List<ExpensesResult>{
                    new ExpensesResult{
                        error = "no Expenses Found",
                        result = false
                    }
                };
            }
            else{
                var result1 = expenses.Select(e => new ExpensesResult{
                    description = e.description,
                    spentMoney = e.spentMoney,
                    CategoryId = e.category.CategoryName,
                    result = true,
                    error = ""
                }).ToList();

                return(result1);
            }
        }

        public async Task<List<ExpensesResult>> OneMounthExpenses()
        {
            var oneMounth = DateTime.UtcNow.AddMonths(-1).Date;
            var expenses = await _context.Tasks
            .Where(x => x.Date.Date == oneMounth)
            .Include(x=>x.category).ToListAsync();
            if(expenses == null || !expenses.Any()){
                return new List<ExpensesResult>{
                    new ExpensesResult{
                        error = "no Expenses Found",
                        result = false
                    }
                };
            }
            else{
                var result1 = expenses.Select(e => new ExpensesResult{
                    description = e.description,
                    spentMoney = e.spentMoney,
                    CategoryId = e.category.CategoryName,
                    result = true,
                    error = ""
                }).ToList();

                return(result1);
            }
        }

        public async Task<List<ExpensesResult>> OneYearExpenses()
        {
            var OneYear = DateTime.UtcNow.AddYears(-1).Date;
            var expenses = await _context.Tasks
            .Where(x => x.Date.Date == OneYear)
            .Include(x=>x.category).ToListAsync();
            if(expenses == null || !expenses.Any()){
                return new List<ExpensesResult>{
                    new ExpensesResult{
                        error = "no Expenses Found",
                        result = false
                    }
                };
            }
            else{
                var result1 = expenses.Select(e => new ExpensesResult{
                    description = e.description,
                    spentMoney = e.spentMoney,
                    CategoryId = e.category.CategoryName,
                    result = true,
                    error = ""
                }).ToList();

                return(result1);
            }
        }

        public async Task<ExpensesResult> RemoveExpenses(ExpensesDto expense)
        {
            var ex = await _context.Tasks.FirstOrDefaultAsync(x => x.description == expense.description);
            if(ex == null){
                return new ExpensesResult{
                    error = "The Item does not Exist",
                    result = false
                };
            }
            else{
                _context.Tasks.Remove(ex);
                await _context.SaveChangesAsync();
                return new ExpensesResult{
                    description = ex.description,
                    result = true
                };
            }
        }

        public async Task<ExpensesResult> updateExpenses(ExpensesDto expense)
        {
            var ex = await _context.Tasks.Include(x=>x.category).FirstOrDefaultAsync(x => x.description == expense.description);
            if(ex == null){
                return new ExpensesResult{
                    error = "The Item does not Exist",
                    result = false
                };
            }
            else{
                ex.description = expense.description;
                ex.spentMoney = expense.spentMoney;
                ex.CategoryId = expense.CategoryId;

                _context.Tasks.Update(ex);
                await _context.SaveChangesAsync();

                return new ExpensesResult{
                    description = ex.description,
                    CategoryId = ex.category.CategoryName,
                    spentMoney = ex.spentMoney,
                    Date = ex.Date,
                    result = true
                };
            }
        }
    }
}