using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Give the token a short lifespan in case the user name and password is compromise
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(_configuration["JWTSettings:Issuer"],
              _configuration["JWTSettings:Audience"],
              claims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWTSettings:AccessTokenDurationInMinutes"])),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
        /// <summary>
        /// Give the token a long life if the user is requesting a token with a refresh token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public string GenerateAccessTokenFromRefreshTokenRequest(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(_configuration["JWTSettings:Issuer"],
              _configuration["JWTSettings:Audience"],
              claims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddDays(int.Parse(_configuration["JWTSettings:RefreshTokenDurationInDays"])),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = _configuration["JWTSettings:Issuer"],
                    ValidAudience = _configuration["JWTSettings:Audience"],
                    ValidateIssuer = true,
                    ValidateAudience = true, //you might want to validate the audience and issuer depending on your use case
                    ValidateLifetime = false, //here we are saying that we don't care about the token's expiration date
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"])),
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
