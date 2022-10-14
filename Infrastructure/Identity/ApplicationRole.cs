using Infrastructure.Enums;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string name) : base(name)
        {

        }
        public Permissions Permissions { get; set; }
    }
}
