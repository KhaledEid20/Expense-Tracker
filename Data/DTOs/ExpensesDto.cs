using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Data.DTOs
{
    public class ExpensesDto
    {
        public double spendedMoney { get; set; }
        public string description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}