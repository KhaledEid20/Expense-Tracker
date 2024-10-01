using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Data.DTOs
{
    public class ExpensesResult
    {
        public string description { get; set; } = string.Empty;
        public double spentMoney { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string CategoryId { get; set; }
        public bool result { get; set; }
        public string error { get; set; } = string.Empty;
    }
}