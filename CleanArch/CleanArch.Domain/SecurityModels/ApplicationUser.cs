using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.SecurityModels
{
    public class ApplicationUser
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Vendor { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenExpiryDate { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
    }
}
