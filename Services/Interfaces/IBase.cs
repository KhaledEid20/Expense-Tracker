using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Services.Interfaces
{
    public interface IBase<T> where T: class
    {
        Task<List<CategoryResult>> GetAllCategories();
        Task<List<ExpensesResult>> GetAllExpenses();
        Task<AuthResultDto> GenerateToken(IdentityUser appuser);
        Task<List<Claim>> GetClaims(IdentityUser user);
    }
}