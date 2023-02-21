using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    /// <summary>
    /// This is up to you to implement everything
    /// </summary>
    public interface IApplicationRoleService
    {
        Task<RoleModel> GetRoleAsync(string roleId);
        Task<bool> AddRoleAsync(RoleModel role);
        Task<bool> RemoveRoleAsync(string id);
    }
}
