using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Services.Interfaces
{
    public interface IAdmin
    {
        Task<string> AddRole(string RoleName);
        Task<string>AddRoleToUser(string email , string RoleName);
    }
}