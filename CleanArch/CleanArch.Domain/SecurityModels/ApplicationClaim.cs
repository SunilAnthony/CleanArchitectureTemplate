using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.SecurityModels
{
    public class ApplicationClaim
    {
        public int Id { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }
}
