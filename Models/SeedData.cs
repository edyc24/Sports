using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sports.Data;
using Sports.Models;
using Sports.Data;

namespace Sports.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider
serviceProvider)
        {
            using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService
            <DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Roles.Any())
                {
                    return; // baza de date contine deja roluri
                }
                context.Roles.AddRange(
                     new IdentityRole { Id = "2c5e174e-3b0e-446f-86af483d56fd7210", Name = "Administrator", NormalizedName = "Administrator".ToUpper() },
                     new IdentityRole { Id = "2c5e174e-3b0e-446f-86af483d56fd7211", Name = "Moderator", NormalizedName = "Moderator".ToUpper() },
                     new IdentityRole { Id = "2c5e174e-3b0e-446f-86af483d56fd7212", Name = "User", NormalizedName = "User".ToUpper() }
                     );
                var hasher = new PasswordHasher<ApplicationUser>();
                context.ApplicationUsers.AddRange(
                     new ApplicationUser
                     {
                         Id = "8e445865-a24d-4543-a6c6-9443d048cdb0",
                         UserName = "admin@test.com",
                         EmailConfirmed = true,
                         NormalizedEmail = "ADMIN@TEST.COM",
                         Email = "admin@test.com",
                         NormalizedUserName = "ADMIN@TEST.COM",
                         PasswordHash = hasher.HashPassword(null,
                    "Admin1!")
                     },
                    new ApplicationUser
                    {
                        Id = "8e445865-a24d-4543-a6c6-9443d048cdb1",
                        UserName = "Moderator@test.com",
                        EmailConfirmed = true,
                        NormalizedEmail = "Moderator@TEST.COM",
                        Email = "Moderator@test.com",
                        NormalizedUserName = "Moderator@TEST.COM",
                        PasswordHash = hasher.HashPassword(null,
                        "Moderator1!")
                    },
                    new ApplicationUser
                    {
                        Id = "8e445865-a24d-4543-a6c6-9443d048cdb2",
                        UserName = "user@test.com",
                        EmailConfirmed = true,
                        NormalizedEmail = "USER@TEST.COM",
                        Email = "user@test.com",
                        NormalizedUserName = "USER@TEST.COM",
                        PasswordHash = hasher.HashPassword(null,
                        "User1!")

                    }
                    );
                context.UserRoles.AddRange(
                     new IdentityUserRole<string>
                     {
                         RoleId = "2c5e174e-3b0e-446f-86af483d56fd7210",
                         UserId = "8e445865-a24d-4543-a6c6-9443d048cdb0"
                     },
                    new IdentityUserRole<string>
                    {
                        RoleId = "2c5e174e-3b0e-446f-86af483d56fd7211",
                        UserId = "8e445865-a24d-4543-a6c6-9443d048cdb1"
                    },
                    new IdentityUserRole<string>
                    {
                        RoleId = "2c5e174e-3b0e-446f-86af483d56fd7212",
                        UserId = "8e445865-a24d-4543-a6c6-9443d048cdb2"
                    }
                     );
                context.SaveChanges();
            }
        }
    }
}

