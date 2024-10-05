using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Services.Interfaces
{
    public interface IClaims
    {
        Task<string> addClaimToUser(string email , string ClaimName , string ClaimValue);
        Task<string> addClaimToRole(string email , string ClaimName , string ClaimValue);
    }
}