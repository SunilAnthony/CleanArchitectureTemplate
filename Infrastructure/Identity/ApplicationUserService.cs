

using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddClaimsToUser(ModifyUserClaim user)
        {
            var appUser = await _userManager.FindByNameAsync(user.Email);
            if (appUser is null)
                return false;
            //Get All their current claims
            var claims = await _userManager.GetClaimsAsync(appUser);

            user.SelectedClaims = user.SelectedClaims ?? new string[] { };

            foreach (var claim in user.SelectedClaims)
            {
                await _userManager.AddClaimAsync(appUser, new System.Security.Claims.Claim(claim, "true"));

            }

            return true;
        }

        public async Task<bool> CreateUser(RegisterUser register)
        {
            var appUser = new ApplicationUser
            {
                Name = register.Name,
                Vendor = register.Vendor,
                UserName = register.Email,
                Email = register.Email,
                EmailConfirmed = true,
                CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name,
                CreatedOn = DateTime.Now

            };
            var result = await _userManager.CreateAsync(appUser, register.Password);
            if (result.Succeeded)
            {
                foreach (var claim in register.Claims)
                {
                    await _userManager.AddClaimAsync(appUser, new System.Security.Claims.Claim(claim, "true"));

                }

                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);

            if (appUser is null)
                return false;
            var result = await _userManager.DeleteAsync(appUser);
            return true;
        }

        public async Task<bool> RemoveClaimsToUser(ModifyUserClaim user)
        {
            var appUser = await _userManager.FindByNameAsync(user.Email);
            if (appUser is null)
                return false;
            //Get All their current claims
            var claims = await _userManager.GetClaimsAsync(appUser);

            user.SelectedClaims = user.SelectedClaims ?? new string[] { };

            foreach (var claim in user.SelectedClaims)
            {
                await _userManager.RemoveClaimAsync(appUser, new System.Security.Claims.Claim(claim, "true"));

            }

            return true;
        }
    }
}
