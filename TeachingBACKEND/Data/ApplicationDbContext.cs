using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Data;

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
    public DbSet<Subjects> Subjects { get; set; }
    public DbSet<LearnHub> LearnHubs { get; set; }
    public DbSet<Link> Links { get; set; }
    public DbSet<Quizz> Quizzes { get; set; }
    public DbSet<RegistrationPlan> RegistrationPlans { get; set; }
    public DbSet<Option> Options { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LearnHub>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");
        modelBuilder.Entity<Link>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");
        modelBuilder.Entity<Quizz>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");


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

        modelBuilder.Entity<Link>()
            .HasOne(link => link.LearnHub)
            .WithMany(learnHub => learnHub.Links)
            .HasForeignKey(link => link.LearnHubId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Quizz>()
            .HasOne(quizz => quizz.Link)
            .WithMany(link => link.Quizzes)
            .HasForeignKey(quizz => quizz.LinkId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Plan)
            .WithMany(p => p.Payments)
            .HasForeignKey(p => p.PlanId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Option>()
        .HasOne(o => o.Quizz)
        .WithMany(q => q.Options)
        .HasForeignKey(o => o.QuizzId)
        .OnDelete(DeleteBehavior.Cascade);

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

        modelBuilder.Entity<RegistrationPlan>().HasData(
    new RegistrationPlan
    {
        Id = Guid.Parse("a1a1a1a1-a1a1-1111-1111-111111111111"),
        RegistrationPlanName = "Bazë",
        Type = "monthly",
        Price = 2000,
        StripeProductName = "Student - Monthly Bazë",
        IsFamilyPlan = false,
        UserType = "student",
        MaxUsers = 1  
    },
    new RegistrationPlan
    {
        Id = Guid.Parse("a1a1a1a1-a1a1-2222-2222-222222222222"),
        RegistrationPlanName = "Standarde",
        Type = "monthly",
        Price = 4000,
        StripeProductName = "Student - Monthly Standarde",
        IsFamilyPlan = false,
        UserType = "student",
        MaxUsers = 1
    },
    new RegistrationPlan
    {
        Id = Guid.Parse("a1a1a1a1-a1a1-3333-3333-333333333333"),
        RegistrationPlanName = "Premium",
        Type = "monthly",
        Price = 6000,
        StripeProductName = "Student - Monthly Premium",
        IsFamilyPlan = false,
        UserType = "student",
        MaxUsers = 1
    },
    new RegistrationPlan
    {
        Id = Guid.Parse("a1a1a1a1-a1a1-4444-4444-444444444444"),
        RegistrationPlanName = "Bazë",
        Type = "yearly",
        Price = 20000,
        StripeProductName = "Student - Yearly Bazë",
        IsFamilyPlan = false,
        UserType = "student",
        MaxUsers = 1
    },
    new RegistrationPlan
    {
        Id = Guid.Parse("a1a1a1a1-a1a1-5555-5555-555555555555"),
        RegistrationPlanName = "Standarde",
        Type = "yearly",
        Price = 40000,
        StripeProductName = "Student - Yearly Standarde",
        IsFamilyPlan = true,
        UserType = "family",
        MaxUsers = 5 
    },
    new RegistrationPlan
    {
        Id = Guid.Parse("a1a1a1a1-a1a1-6666-6666-666666666666"),
        RegistrationPlanName = "Premium",
        Type = "yearly",
        Price = 60000,
        StripeProductName = "Student - Yearly Premium",
        IsFamilyPlan = true,
        UserType = "family",
        MaxUsers = 10
    },
    new RegistrationPlan
    {
        Id = Guid.Parse("a1a1a1a1-a1a1-7777-7777-777777777777"),
        RegistrationPlanName = "Supervisor - Monthly",
        Type = "monthly",
        Price = 10000,
        StripeProductName = "Supervisor - Monthly Plan",
        IsFamilyPlan = false,
        UserType = "supervisor",
        MaxUsers = 50  
    },
    new RegistrationPlan
    {
        Id = Guid.Parse("a1a1a1a1-a1a1-8888-8888-888888888888"),
        RegistrationPlanName = "Supervisor - Yearly",
        Type = "yearly",
        Price = 100000,
        StripeProductName = "Supervisor - Yearly Plan",
        IsFamilyPlan = false,
        UserType = "supervisor",
        MaxUsers = 500
    }
);
        modelBuilder.Entity<Subjects>().HasData(
            new Subjects { Id = Guid.Parse("dbe6757d-2138-463a-bee7-5d07a6d7b320"), Name = "Letërsi" },
            new Subjects { Id = Guid.Parse("616273bd-2a2a-4894-b689-57fe86702ae0"), Name = "Matematik" },
            new Subjects { Id = Guid.Parse("a072e5ed-714d-40d3-9af8-3b5b940acd2f"), Name = "Anglisht" },
            new Subjects { Id = Guid.Parse("a5cf5e27-ef08-4fef-b907-109496b284eb"), Name = "Histori" },
            new Subjects { Id = Guid.Parse("5eac82ae-0b4b-47a8-9871-ba6ab1c99df7"), Name = "Gjeografi" },
            new Subjects { Id = Guid.Parse("faf6b93a-91d1-4ead-85f5-0120ac85f7d2"), Name = "Shkenca" }
        );

        var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var adminEmail = "admin@teachapp.com";
        var adminPassword = BCrypt.Net.BCrypt.HashPassword("Admin!2025", 12);

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