using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{

    public class TokenService : ITokenService
    {
        private readonly IJWTService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IJWTService tokenService, UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           RoleManager<ApplicationRole> roleManager,
            IConfiguration config,
            IHttpContextAccessor httpContextAccessor)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<TokenResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                // Optional: Block request if token hasn't expired yet
                //if (user.TokenExpiryDate > DateTime.UtcNow) return BadRequest($"Access token hasn't expired yet. Only one request allowed every {_config["JWTSettings: AccessTokenDurationInMinutes"]} minutes.");
                if (result.Succeeded)
                {
                    IdentityOptions _options = new IdentityOptions();
                    var claims = new List<Claim>
                        {
                          new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                          new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName),
                        };
                    // Get the roles for the user
                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var userRole in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    //This will add Claims
                    var userClaims = await _userManager.GetClaimsAsync(user); //User Claims from Identity
                    claims.AddRange(userClaims);
                    var jwtToken = _tokenService.GenerateAccessToken(claims);

                    var refreshToken = _tokenService.GenerateRefreshToken();

                    user.RefreshToken = refreshToken;
                    user.TokenExpiryDate = DateTime.UtcNow.AddMinutes(int.Parse(_config["JWTSettings:AccessTokenDurationInMinutes"]));
                    // Invalid the refresh token date so a user can still do a refresh if the haven't used a refresh token in over 14 days
                    user.RefreshTokenExpiryDate = null;
                    await _userManager.UpdateAsync(user);

                    return new TokenResult
                    {
                        Token = jwtToken,
                        RefreshToken = refreshToken,
                        TokenType = "Bearer",
                        ExpiresOn = DateTime.UtcNow.AddMinutes(int.Parse(_config["JWTSettings:AccessTokenDurationInMinutes"]))
                    };
                }
            }
            return null;
        }

        public async Task<RefreshTokenResult> Refresh(string Token, string RefreshToken)
        {
            // Get the principal claims from the original token
            var principal = _tokenService.GetPrincipalFromExpiredToken(Token);
            if (principal == null) return new RefreshTokenResult(null, "Access Token Fail Validation.");
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == username);

            if (user == null || user.RefreshToken != RefreshToken) return new RefreshTokenResult(null, "Invalid refresh token.");

            // Optional: Here you can prevent a refresh token request if the token has not expired yet
            // if (user.TokenExpiryDate > DateTime.UtcNow) return BadRequest("This access token hasn't expired yet.");

            // A better way to do the above is to check the expiry date on the original token
            // This will only allow one refresh token request because of the second last check
            var expiryDate = long.Parse(principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDate);
            if (expiryDateUtc > DateTime.UtcNow) return new RefreshTokenResult(null, "This access token hasn't expired yet.");

            // Reject a refresh if the refresh token expired
            if (DateTime.UtcNow > user.RefreshTokenExpiryDate) return new RefreshTokenResult(null, "This refresh token has expired");

            // Assign the previous token claims to the new token
            var newJwtToken = _tokenService.GenerateAccessTokenFromRefreshTokenRequest(principal.Claims);
            // Generate a new refresh token
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            //Replace the expiry date with the new expiry date from the Refresh Token request
            user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(int.Parse(_config["JWTSettings:RefreshTokenDurationInDays"]));
            // Store the new refresh token in the user record
            await _userManager.UpdateAsync(user);

            return new RefreshTokenResult(new TokenResult
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken,
                TokenType = "Bearer",
                ExpiresOn = DateTime.UtcNow.AddDays(int.Parse(_config["JWTSettings:RefreshTokenDurationInDays"]))
            }, string.Empty);
        }

        public async Task<bool> Revoke()
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;

            var user = _userManager.Users.SingleOrDefault(u => u.UserName == username);
            if (user == null)
                return false;

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return true;
        }
    }
}
