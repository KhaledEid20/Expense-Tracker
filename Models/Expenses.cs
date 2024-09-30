using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Models
{
    public class Expenses
    {
        public int Id { get; set; }
        public double spendedMoney { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}