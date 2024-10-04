using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Data.DTOs
{
    public class AuthToken
    {
        public string Token { get; set; }
        public string RefreshToke { get; set; }
    }
}