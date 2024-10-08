using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expense_Tracker.Data;
using Expense_Tracker.Data.DTOs;
using Expense_Tracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Services
{
    public class CategoryRepo : Base<Category> , ICategory
    {
        public CategoryRepo(AppDbContext context , UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration) : base(context , userManager , roleManager ,configuration)
        {

        }
        public async Task<CategoryResult> AppendCategory(CategoryDto category)
        {
            var cat = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == category.CategoryName);
            if(cat != null){
                return new CategoryResult{
                    result = false
                };
            }
            else{
                var newCat = new Category(){
                    CategoryName = category.CategoryName
                };
                await _context.Categories.AddAsync(newCat);
                await _context.SaveChangesAsync();
                return new CategoryResult{
                    CategoryName = category.CategoryName + " Succefully Added",
                    result = true
                }; 
            }
        }

        public async Task<CategoryResult> DeleteCategory(CategoryDto category)
        {
            var cat = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == category.CategoryName);
            if(cat==null){
                return new CategoryResult{
                    result = false
                };
            }
            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return new CategoryResult{
                CategoryName = category.CategoryName + " Succefully Removed",
                result = true
                }; 
        }

        public Task<List<CategoryResult>> GetAllCategory()
        {
            return GetAllCategories();
        }
    }
}