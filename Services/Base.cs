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
    }
}