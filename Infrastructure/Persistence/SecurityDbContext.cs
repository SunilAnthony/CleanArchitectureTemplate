using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class SecurityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Add this for identity
            base.OnModelCreating(builder);


        }
    }
}
