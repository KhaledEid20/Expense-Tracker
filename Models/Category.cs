using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Models
{
    public class Category{
        [Key]
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public ICollection<Expenses> expenses {get; set;}
    }
}