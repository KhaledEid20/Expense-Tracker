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

        public UnitOfWork(IAccount account , IAdmin admin , ICategory category , IExpenses expenses , IClaims claims , AppDbContext context)
        {
            account = account;
            admin = admin;
            category = category;
            expenses = expenses;
            claims = claims;
            _context = context;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}