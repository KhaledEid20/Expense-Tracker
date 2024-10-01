using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Data.DTOs
{
    public class ExpensesDto
    {
        public string description { get; set; } = string.Empty;
        public double spentMoney { get; set; }
        public int CategoryId { get; set; }
    }
}