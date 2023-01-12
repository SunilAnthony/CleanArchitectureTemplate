using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class TokenResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
