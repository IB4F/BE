using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                // Primary key
                entity.HasKey(u => u.Id);

                // Email is required and can be limited to 256 characters
                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(256);

                // PasswordHash is required
                entity.Property(u => u.PasswordHash)
                      .IsRequired();
            });
        }
    }
}
