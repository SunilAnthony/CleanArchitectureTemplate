using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class RefreshTokenResult
    {
        public TokenResult Result { get; set; }
        public string Message { get; set; }
        public RefreshTokenResult(TokenResult result, string message)
        {
            Result = result;
            Message = message;
        }
    }
}
