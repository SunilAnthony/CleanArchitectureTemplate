using Infrastructure.Authorization;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public ApplicationUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
    : base(userManager, roleManager, optionsAccessor)
        { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var userRoleNames = await UserManager.GetRolesAsync(user) ?? Array.Empty<string>();

            var userRoles = await RoleManager.Roles.Where(r =>
                userRoleNames.Contains(r.Name ?? string.Empty)).ToListAsync();

            var userPermissions = Permissions.None;

            foreach (var role in userRoles)
                userPermissions |= role.Permissions;

            var permissionsValue = (int)userPermissions;

            identity.AddClaim(
                new Claim(CustomClaimTypes.Permissions, permissionsValue.ToString()));

            return identity;
        }
    }
}
