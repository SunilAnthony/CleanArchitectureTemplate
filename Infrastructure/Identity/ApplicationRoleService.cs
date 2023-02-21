using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ApplicationRoleService : IApplicationRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationRoleService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> AddRoleAsync(RoleModel role)
        {
            var appRole = new ApplicationRole(
                role.Name,
                role.Description );
            var roleResult = await _roleManager.CreateAsync(appRole);

            if (!roleResult.Succeeded)
                return false;
            else
                return true;
        }

        public Task<RoleModel> GetRoleAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveRoleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return false;
            await _roleManager.DeleteAsync(role);

            return true;
        }
    }
}
