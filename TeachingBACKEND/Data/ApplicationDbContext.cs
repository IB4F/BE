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
        public DbSet<Payment> Payments { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Class> Classes { get; set; }

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

            modelBuilder.Entity<Payment>()
           .HasOne(p => p.User)
           .WithMany(u => u.Payments)
           .HasForeignKey(p => p.UserId)
           .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<City>().HasData(
            new City { Id = Guid.Parse("a2d4a4ee-5fa2-4a33-bd3c-2bbf98e9310b"), Name = "Tirana" },
            new City { Id = Guid.Parse("b8e13e5a-bba4-48b6-99d6-c4f123ab2cb3"), Name = "Durrës" },
            new City { Id = Guid.Parse("d05f0e99-20f0-4c9a-b03f-3ea92ec02b41"), Name = "Shkodër" },
            new City { Id = Guid.Parse("bc4e14b5-7d7e-4b6c-8b33-eaa2c91f9015"), Name = "Vlorë" },
            new City { Id = Guid.Parse("2dff0b99-3d86-47a5-b7ad-4e6d3c0ec748"), Name = "Elbasan" },
            new City { Id = Guid.Parse("12f4c32b-4a42-4b1f-9247-9b40efb21363"), Name = "Fier" },
            new City { Id = Guid.Parse("f8d8e184-74c0-4551-b66b-7ae3f05e7ff5"), Name = "Korçë" },
            new City { Id = Guid.Parse("69e02c84-4a61-406e-844d-24df2e25a983"), Name = "Gjirokastër" },
            new City { Id = Guid.Parse("8e065f0d-71c1-4a63-804b-49b1d08c1407"), Name = "Berat" },
            new City { Id = Guid.Parse("d28f0be5-5f17-4ec8-b365-7387c22234e9"), Name = "Lezhë" },
            new City { Id = Guid.Parse("e5f957d6-7a31-45b2-bded-44e4992d4b83"), Name = "Kukës" }
            );



            modelBuilder.Entity<Class>().HasData(
            new Class { Id = Guid.Parse("e3f9a8f1-9c4e-4a91-8bcb-0b6b1583d3a1"), Name = "Klasa 1" },
            new Class { Id = Guid.Parse("a61d58b7-23f8-48f7-9778-3e048c5808a0"), Name = "Klasa 2" },
            new Class { Id = Guid.Parse("81bce1db-4f7c-4f6f-9e59-cde56a8200b6"), Name = "Klasa 3" },
            new Class { Id = Guid.Parse("f82a2ea4-c4c9-4895-9484-0197a299c02f"), Name = "Klasa 4" },
            new Class { Id = Guid.Parse("10840373-f0f4-4b10-9ee0-c5a831b6cf6a"), Name = "Klasa 5" },
            new Class { Id = Guid.Parse("7fc8018d-6310-4c1f-b878-4b3a5b0b265c"), Name = "Klasa 6" },
            new Class { Id = Guid.Parse("8e77a87f-e42b-487a-9cde-54f648c8c457"), Name = "Klasa 7" },
            new Class { Id = Guid.Parse("43e7b804-0e1a-4c82-9e44-6a194ee1ff63"), Name = "Klasa 8" },
            new Class { Id = Guid.Parse("fd4b14ea-1b79-4e4c-83e0-0196c55b4bc1"), Name = "Klasa 9" },
            new Class { Id = Guid.Parse("1c0b8bb7-9eb9-4a4d-8c4d-67de8057ae49"), Name = "Klasa 10" },
            new Class { Id = Guid.Parse("3402fc90-d7be-420e-a980-2ff430d84838"), Name = "Klasa 11" },
            new Class { Id = Guid.Parse("013d0df5-50ef-4269-8a12-9b4f91ef07e1"), Name = "Klasa 12" }
            );



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
