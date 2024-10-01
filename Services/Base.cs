using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Expense_Tracker.Services
{
    public class Base<T> : IBase<T> where T : class
    {
        public AppDbContext _context {get; set;}
        public Base(AppDbContext context)
        {
            _context = context;
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
    }
}