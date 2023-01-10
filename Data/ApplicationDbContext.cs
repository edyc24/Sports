using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sports.Models;

namespace Sports.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Discussion>()
                .HasOne(d => d.Category)
                .WithMany(c => c.Discussions)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Comment>()
                .HasOne(c => c.Discussion)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.DiscussionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

           
        }
    }
}