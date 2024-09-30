using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expense_Tracker.Data.DTOs;

namespace Expense_Tracker.Services.Interfaces
{
    public interface ICategory
    {
        Task<CategoryResult> AppendCategory(CategoryDto category);
        Task<CategoryResult> DeleteCategory(CategoryDto category);
        Task<List<CategoryResult>> GetAllCategory();
    }
}