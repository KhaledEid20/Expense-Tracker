using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expense_Tracker.Data;
using Expense_Tracker.Services.Interfaces;

namespace Expense_Tracker.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAccount account { get; set; }
        public IAdmin admin { get; set ; }
        public ICategory category { get; set ; }
        public IExpenses expenses { get  ; set  ; }
        public IClaims claims { get  ; set  ; }
        public AppDbContext _context {get; set;}

        public UnitOfWork(IAccount _account , IAdmin _admin , ICategory _category , IExpenses _expenses , IClaims _claims , AppDbContext context)
        {
            _account = account;
            _admin = admin;
            _category = category;
            _expenses = expenses;
            _claims = claims;
            context = _context;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}