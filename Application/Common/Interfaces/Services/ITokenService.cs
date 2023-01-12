using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface ITokenService
    {
        Task<TokenResult> Login(string email, string password);
        Task<RefreshTokenResult> Refresh(string Token, string RefreshToken);
        Task<bool> Revoke();
    }
}
