using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Models
{
    public class RefreshToken
    {
        [Key]
        public int ID { get; set; }
        public string refreshToken { get; set; }
        public string AccessTokenId {get; set;}
        public string UserID { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; } = DateTime.Now;
    }
}