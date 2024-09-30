using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Services.Interfaces
{
    public interface IBase<T> where T: class
    {
        Task<List<CategoryResult>> GetAllCategories();

    }
}