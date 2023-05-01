using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var superAdminRoleId = "171663b8-143e-42e9-beb1-612d9d46e4da";
            var adminRoleId = "bace0daa-8fe8-4e16-a6ce-0a3e00e05808";
            var userRoleId = "7f44fd66-277b-4f0d-9a6b-2293f47c8810";


            // Seed Roles (User, Admin, SuperAdmin
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id= superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id= adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id= userRoleId,
                    ConcurrencyStamp = userRoleId
                }


            };
            builder.Entity<IdentityRole>().HasData(roles);

            // Seed Super Admin User
            var superAdminId = "088b9f28-1d5c-4f3c-9566-d1f618c2b99a";
            var superAdminUser = new IdentityUser()
            {
                Id = superAdminId,
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com"
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "superadmin123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            // Add All Roles To Super Admin User
            var superAdminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId= superAdminRoleId,
                    UserId= superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId= adminRoleId,
                    UserId= superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId= userRoleId,
                    UserId= superAdminId
                }

            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }

}
