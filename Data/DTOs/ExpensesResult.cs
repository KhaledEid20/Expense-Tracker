using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Data.DTOs
{
    public class ExpensesResult
    {
        public double spentMoney { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}