using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Services.Interfaces
{
    public interface IAccount
    {
        Task<AuthResultDto> register(registerDTO user);
        Task<AuthResultDto> login(LoginDTO user);
        Task<AuthResultDto> Refresh (AuthToken authResult);
    }
}