using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IApplicationUserService
    {
        Task<bool> CreateUser(RegisterUser user);
        Task<bool> AddClaimsToUser(ModifyUserClaim user);
        Task<bool> RemoveClaimsToUser(ModifyUserClaim user);
        Task<bool> DeleteUser(string id);
    }
}
