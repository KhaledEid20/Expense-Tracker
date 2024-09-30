using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccount account {get; set;}
        IAdmin admin {get; set;}
        ICategory category{get; set;}
        IExpenses expenses{get; set;}
        IClaims claims {get; set;}
        
    }
}