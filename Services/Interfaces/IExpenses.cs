using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Services.Interfaces
{
    public interface IExpenses
    {
        Task<ExpensesResult> AddExpenses(ExpensesDto expense);
        Task<ExpensesResult> RemoveExpenses(ExpensesDto expense);
        Task<ExpensesResult> updateExpenses(ExpensesDto expense);
        Task<List<ExpensesResult>> OneDayExpenses();
        Task<List<ExpensesResult>> OneMounthExpenses();
        Task<List<ExpensesResult>> OneYearExpenses();
        Task<List<ExpensesResult>> GetExpenses();
    }
}