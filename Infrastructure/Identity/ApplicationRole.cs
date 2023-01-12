using Microsoft.AspNetCore.Identity;


namespace Infrastructure.Identity
{
    public sealed class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string name, string description) : base(name)
        {
            Description= description;
        }
        public string Description { get; set; }
    }
}
