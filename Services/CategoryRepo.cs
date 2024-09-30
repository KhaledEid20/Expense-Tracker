using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expense_Tracker.Data;
using Expense_Tracker.Services.Interfaces;

namespace Expense_Tracker.Services
{
    public class CategoryRepo : Base<Category> , ICategory
    {
        public CategoryRepo(AppDbContext context) : base(context)
        {
            
        }
    }
}