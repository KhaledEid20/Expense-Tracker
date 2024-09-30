using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expense_Tracker.Data;
using Expense_Tracker.Services.Interfaces;

namespace Expense_Tracker.Services
{
    public class Base<T> : IBase<T> where T : class
    {
        public AppDbContext _context {get; set;}
        public Base(AppDbContext context)
        {
            context = _context;
        }
    }
}