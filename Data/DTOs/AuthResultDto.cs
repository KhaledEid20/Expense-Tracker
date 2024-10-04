using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Data.DTOs
{
    public class AuthResultDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken {get; set;} = string.Empty;
        public bool result { get; set; }
        public string error { get; set; }
    }
}