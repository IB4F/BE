using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

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

            var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111"); 
            var adminEmail = "admin@teachapp.com";
            var adminPassword = BCrypt.Net.BCrypt.HashPassword("Admin!2025", workFactor: 12); 

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                Email = adminEmail,
                PasswordHash = adminPassword,
                Role = UserRole.Admin,
                FirstName = "System",
                LastName = "Administrator",
                DateOfBirth = new DateTime(1985, 1, 1),
                School = "Main Admin Office",
                City = "Tirana",
                PhoneNumber = "+35500000000",
                Profession = "Administrator",
                CurrentClass = null,
                IsEmailVerified = true,
                ApprovalStatus = ApprovalStatus.Approved,
                EmailVerificationToken = null,
                EmailVerificationTokenExpiry = null,
                PasswordResetToken = null,
                PasswordResetTokenExpiry = null,
                RefreshToken = Guid.Empty,
                RefreshTokenExpiry = DateTime.MinValue
            });
        }
    }
}
