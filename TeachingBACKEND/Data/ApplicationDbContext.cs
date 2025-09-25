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
    public DbSet<SupervisorApplication> SupervisorApplications { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Subjects> Subjects { get; set; }
    public DbSet<LearnHub> LearnHubs { get; set; }
    public DbSet<Link> Links { get; set; }
    public DbSet<Quizz> Quizzes { get; set; }
    public DbSet<SubscriptionPackage> SubscriptionPackages { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<QuizType> QuizTypes { get; set; }
    public DbSet<UploadedFile> Files { get; set; }
    public DbSet<StudentQuizPerformance> StudentQuizPerformances { get; set; }
    public DbSet<StudentQuizSession> StudentQuizSessions { get; set; }
    public DbSet<StudentPerformanceSummary> StudentPerformanceSummaries { get; set; }

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
                
            // Configure ActiveSubscription relationship
            entity.HasOne(u => u.ActiveSubscription)
                .WithOne()
                .HasForeignKey<User>(u => u.ActiveSubscriptionId)
                .OnDelete(DeleteBehavior.SetNull);
                
            // Configure Supervisor-Student relationship
            entity.HasOne(u => u.Supervisor)
                .WithMany(s => s.SupervisedUsers)
                .HasForeignKey(u => u.SupervisorId)
                .OnDelete(DeleteBehavior.SetNull);
                
            // Add indexes for performance
            entity.HasIndex(u => u.Email)
                .IsUnique();
                
            entity.HasIndex(u => u.SupervisorId);
            
            entity.HasIndex(u => new { u.SupervisorId, u.Role });
        });

        modelBuilder.Entity<SupervisorApplication>(entity =>
        {
            // Primary key
            entity.HasKey(sa => sa.Id);

            // Required fields
            entity.Property(sa => sa.SchoolName)
                .IsRequired()
                .HasMaxLength(256);
                
            entity.Property(sa => sa.ContactPersonFirstName)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(sa => sa.ContactPersonLastName)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(sa => sa.ContactPersonEmail)
                .IsRequired()
                .HasMaxLength(256);
                
            entity.Property(sa => sa.ContactPersonPhone)
                .IsRequired()
                .HasMaxLength(20);
                
            entity.Property(sa => sa.City)
                .IsRequired()
                .HasMaxLength(100);

            // Optional fields
            entity.Property(sa => sa.Address)
                .HasMaxLength(500);
                
            entity.Property(sa => sa.AdditionalInfo)
                .HasMaxLength(1000);
                
            entity.Property(sa => sa.RejectionReason)
                .HasMaxLength(1000);

            // Configure relationship with User (if approved)
            entity.HasOne(sa => sa.ApprovedUser)
                .WithOne()
                .HasForeignKey<SupervisorApplication>(sa => sa.ApprovedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Add indexes for performance
            entity.HasIndex(sa => sa.ContactPersonEmail);
            entity.HasIndex(sa => sa.ApprovalStatus);
            entity.HasIndex(sa => sa.ApplicationDate);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(p => p.Id);
            
            entity.HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.SetNull);
                
            entity.HasOne(p => p.SubscriptionPackage)
                .WithMany(sp => sp.Payments)
                .HasForeignKey(p => p.SubscriptionPackageId)
                .OnDelete(DeleteBehavior.Cascade);
        });

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

        // LearnHub tier configuration
        modelBuilder.Entity<LearnHub>()
            .Property(lh => lh.RequiredTier)
            .HasConversion<string>();

        // Subscription entity configurations
        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(s => s.Id);
            
            entity.HasOne(s => s.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(s => s.SubscriptionPackage)
                .WithMany(sp => sp.Subscriptions)
                .HasForeignKey(s => s.SubscriptionPackageId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.Property(s => s.StripeSubscriptionId)
                .IsRequired()
                .HasMaxLength(255);
                
            entity.Property(s => s.StripeCustomerId)
                .IsRequired()
                .HasMaxLength(255);
                
            entity.Property(s => s.StripePriceId)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<SubscriptionPayment>(entity =>
        {
            entity.HasKey(sp => sp.Id);
            
            entity.HasOne(sp => sp.Subscription)
                .WithMany(s => s.Payments)
                .HasForeignKey(sp => sp.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.Property(sp => sp.StripePaymentIntentId)
                .IsRequired()
                .HasMaxLength(255);
                
            entity.Property(sp => sp.StripeInvoiceId)
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Option>()
        .HasOne(o => o.Quizz)
        .WithMany(q => q.Options)
        .HasForeignKey(o => o.QuizzId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Option>()
        .HasOne(o => o.OptionImage)
        .WithMany()
        .HasForeignKey(o => o.OptionImageId)
        .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Quizz>()
        .HasOne(q => q.QuizzType)
        .WithMany()
        .HasForeignKey(q => q.QuizzTypeId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Quizz>()
        .HasOne(q => q.QuestionAudio)
        .WithMany()
        .HasForeignKey(q => q.QuestionAudioId)
        .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Quizz>()
        .HasOne(q => q.ExplanationAudio)
        .WithMany()
        .HasForeignKey(q => q.ExplanationAudioId)
        .OnDelete(DeleteBehavior.NoAction);

        // Self-referencing relationship for parent-child quizzes
        modelBuilder.Entity<Quizz>()
            .HasOne(q => q.ParentQuiz)
            .WithMany(q => q.ChildQuizzes)
            .HasForeignKey(q => q.ParentQuizId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<StudentQuizPerformance>(entity =>
        {
            entity.HasKey(sqp => sqp.Id);
            
            entity.HasOne(sqp => sqp.Student)
                .WithMany()
                .HasForeignKey(sqp => sqp.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
                
            entity.HasOne(sqp => sqp.Quiz)
                .WithMany()
                .HasForeignKey(sqp => sqp.QuizId)
                .OnDelete(DeleteBehavior.NoAction);
                
            entity.HasOne(sqp => sqp.Link)
                .WithMany()
                .HasForeignKey(sqp => sqp.LinkId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<StudentQuizSession>(entity =>
        {
            entity.HasKey(sqs => sqs.Id);
            
            entity.HasOne(sqs => sqs.Student)
                .WithMany()
                .HasForeignKey(sqs => sqs.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
                
            entity.HasOne(sqs => sqs.Quiz)
                .WithMany()
                .HasForeignKey(sqs => sqs.QuizId)
                .OnDelete(DeleteBehavior.NoAction);
                
            entity.HasOne(sqs => sqs.Link)
                .WithMany()
                .HasForeignKey(sqs => sqs.LinkId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<StudentPerformanceSummary>(entity =>
        {
            entity.HasKey(sps => sps.Id);
            
            entity.HasOne(sps => sps.Student)
                .WithMany()
                .HasForeignKey(sps => sps.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
                
            entity.HasOne(sps => sps.Link)
                .WithMany()
                .HasForeignKey(sps => sps.LinkId)
                .OnDelete(DeleteBehavior.NoAction);
        });

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


        // SubscriptionPackage Seed Data - 18 packages total
        modelBuilder.Entity<SubscriptionPackage>().HasData(
            // Student Packages (Fixed Pricing)
            new SubscriptionPackage
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Student Basic Monthly",
                Description = "Basic student package with monthly billing",
                UserType = UserType.Student,
                Tier = PackageTier.Basic,
                BillingInterval = BillingInterval.Month,
                MonthlyPrice = 2000, // €20.00
                YearlyPrice = 20000, // €200.00
                StripeMonthlyPriceId = "price_student_basic_monthly",
                StripeYearlyPriceId = "price_student_basic_yearly",
                MaxUsers = 1,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("11111111-1111-2222-2222-222222222222"),
                Name = "Student Basic Yearly",
                Description = "Basic student package with yearly billing",
                UserType = UserType.Student,
                Tier = PackageTier.Basic,
                BillingInterval = BillingInterval.Year,
                MonthlyPrice = 2000, // €20.00
                YearlyPrice = 20000, // €200.00
                StripeMonthlyPriceId = "price_student_basic_monthly",
                StripeYearlyPriceId = "price_student_basic_yearly",
                MaxUsers = 1,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("22222222-2222-1111-1111-111111111111"),
                Name = "Student Standard Monthly",
                Description = "Standard student package with monthly billing",
                UserType = UserType.Student,
                Tier = PackageTier.Standard,
                BillingInterval = BillingInterval.Month,
                MonthlyPrice = 4000, // €40.00
                YearlyPrice = 40000, // €400.00
                StripeMonthlyPriceId = "price_student_standard_monthly",
                StripeYearlyPriceId = "price_student_standard_yearly",
                MaxUsers = 1,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Student Standard Yearly",
                Description = "Standard student package with yearly billing",
                UserType = UserType.Student,
                Tier = PackageTier.Standard,
                BillingInterval = BillingInterval.Year,
                MonthlyPrice = 4000, // €40.00
                YearlyPrice = 40000, // €400.00
                StripeMonthlyPriceId = "price_student_standard_monthly",
                StripeYearlyPriceId = "price_student_standard_yearly",
                MaxUsers = 1,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("33333333-3333-1111-1111-111111111111"),
                Name = "Student Premium Monthly",
                Description = "Premium student package with monthly billing",
                UserType = UserType.Student,
                Tier = PackageTier.Premium,
                BillingInterval = BillingInterval.Month,
                MonthlyPrice = 6000, // €60.00
                YearlyPrice = 60000, // €600.00
                StripeMonthlyPriceId = "price_student_premium_monthly",
                StripeYearlyPriceId = "price_student_premium_yearly",
                MaxUsers = 1,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("33333333-3333-2222-2222-222222222222"),
                Name = "Student Premium Yearly",
                Description = "Premium student package with yearly billing",
                UserType = UserType.Student,
                Tier = PackageTier.Premium,
                BillingInterval = BillingInterval.Year,
                MonthlyPrice = 6000, // €60.00
                YearlyPrice = 60000, // €600.00
                StripeMonthlyPriceId = "price_student_premium_monthly",
                StripeYearlyPriceId = "price_student_premium_yearly",
                MaxUsers = 1,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // Family Packages (Dynamic Pricing)
            new SubscriptionPackage
            {
                Id = Guid.Parse("44444444-4444-1111-1111-111111111111"),
                Name = "Family Basic Monthly",
                Description = "Basic family package with monthly billing - dynamic pricing based on family size",
                UserType = UserType.Family,
                Tier = PackageTier.Basic,
                BillingInterval = BillingInterval.Month,
                MonthlyPrice = 3000, // €30.00 base
                YearlyPrice = 30000, // €300.00 base
                BasePrice = 3000, // €30.00 base price
                PricePerAdditionalMember = 1000, // €10.00 per additional member
                MinFamilyMembers = 1,
                MaxFamilyMembers = 10,
                StripeMonthlyPriceId = "price_family_basic_monthly",
                StripeYearlyPriceId = "price_family_basic_yearly",
                MaxUsers = 10,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("44444444-4444-2222-2222-222222222222"),
                Name = "Family Basic Yearly",
                Description = "Basic family package with yearly billing - dynamic pricing based on family size",
                UserType = UserType.Family,
                Tier = PackageTier.Basic,
                BillingInterval = BillingInterval.Year,
                MonthlyPrice = 3000, // €30.00 base
                YearlyPrice = 30000, // €300.00 base
                BasePrice = 30000, // €300.00 base price
                PricePerAdditionalMember = 2000, // €20.00 per additional member
                MinFamilyMembers = 1,
                MaxFamilyMembers = 10,
                StripeMonthlyPriceId = "price_family_basic_monthly",
                StripeYearlyPriceId = "price_family_basic_yearly",
                MaxUsers = 10,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("55555555-5555-1111-1111-111111111111"),
                Name = "Family Standard Monthly",
                Description = "Standard family package with monthly billing - dynamic pricing based on family size",
                UserType = UserType.Family,
                Tier = PackageTier.Standard,
                BillingInterval = BillingInterval.Month,
                MonthlyPrice = 5000, // €50.00 base
                YearlyPrice = 50000, // €500.00 base
                BasePrice = 5000, // €50.00 base price
                PricePerAdditionalMember = 1000, // €10.00 per additional member
                MinFamilyMembers = 1,
                MaxFamilyMembers = 10,
                StripeMonthlyPriceId = "price_family_standard_monthly",
                StripeYearlyPriceId = "price_family_standard_yearly",
                MaxUsers = 10,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("55555555-5555-2222-2222-222222222222"),
                Name = "Family Standard Yearly",
                Description = "Standard family package with yearly billing - dynamic pricing based on family size",
                UserType = UserType.Family,
                Tier = PackageTier.Standard,
                BillingInterval = BillingInterval.Year,
                MonthlyPrice = 5000, // €50.00 base
                YearlyPrice = 50000, // €500.00 base
                BasePrice = 50000, // €500.00 base price
                PricePerAdditionalMember = 2000, // €20.00 per additional member
                MinFamilyMembers = 1,
                MaxFamilyMembers = 10,
                StripeMonthlyPriceId = "price_family_standard_monthly",
                StripeYearlyPriceId = "price_family_standard_yearly",
                MaxUsers = 10,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("66666666-6666-1111-1111-111111111111"),
                Name = "Family Premium Monthly",
                Description = "Premium family package with monthly billing - dynamic pricing based on family size",
                UserType = UserType.Family,
                Tier = PackageTier.Premium,
                BillingInterval = BillingInterval.Month,
                MonthlyPrice = 8000, // €80.00 base
                YearlyPrice = 80000, // €800.00 base
                BasePrice = 8000, // €80.00 base price
                PricePerAdditionalMember = 1000, // €10.00 per additional member
                MinFamilyMembers = 1,
                MaxFamilyMembers = 10,
                StripeMonthlyPriceId = "price_family_premium_monthly",
                StripeYearlyPriceId = "price_family_premium_yearly",
                MaxUsers = 10,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("66666666-6666-2222-2222-222222222222"),
                Name = "Family Premium Yearly",
                Description = "Premium family package with yearly billing - dynamic pricing based on family size",
                UserType = UserType.Family,
                Tier = PackageTier.Premium,
                BillingInterval = BillingInterval.Year,
                MonthlyPrice = 8000, // €80.00 base
                YearlyPrice = 80000, // €800.00 base
                BasePrice = 80000, // €800.00 base price
                PricePerAdditionalMember = 2000, // €20.00 per additional member
                MinFamilyMembers = 1,
                MaxFamilyMembers = 10,
                StripeMonthlyPriceId = "price_family_premium_monthly",
                StripeYearlyPriceId = "price_family_premium_yearly",
                MaxUsers = 10,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // Supervisor Packages (Fixed Pricing)
            new SubscriptionPackage
            {
                Id = Guid.Parse("77777777-7777-1111-1111-111111111111"),
                Name = "Supervisor Basic Monthly",
                Description = "Basic supervisor package with monthly billing",
                UserType = UserType.Supervisor,
                Tier = PackageTier.Basic,
                BillingInterval = BillingInterval.Month,
                MonthlyPrice = 10000, // €100.00
                YearlyPrice = 100000, // €1000.00
                StripeMonthlyPriceId = "price_supervisor_basic_monthly",
                StripeYearlyPriceId = "price_supervisor_basic_yearly",
                MaxUsers = 50,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("77777777-7777-2222-2222-222222222222"),
                Name = "Supervisor Basic Yearly",
                Description = "Basic supervisor package with yearly billing",
                UserType = UserType.Supervisor,
                Tier = PackageTier.Basic,
                BillingInterval = BillingInterval.Year,
                MonthlyPrice = 10000, // €100.00
                YearlyPrice = 100000, // €1000.00
                StripeMonthlyPriceId = "price_supervisor_basic_monthly",
                StripeYearlyPriceId = "price_supervisor_basic_yearly",
                MaxUsers = 50,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("88888888-8888-1111-1111-111111111111"),
                Name = "Supervisor Standard Monthly",
                Description = "Standard supervisor package with monthly billing",
                UserType = UserType.Supervisor,
                Tier = PackageTier.Standard,
                BillingInterval = BillingInterval.Month,
                MonthlyPrice = 20000, // €200.00
                YearlyPrice = 200000, // €2000.00
                StripeMonthlyPriceId = "price_supervisor_standard_monthly",
                StripeYearlyPriceId = "price_supervisor_standard_yearly",
                MaxUsers = 100,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("88888888-8888-2222-2222-222222222222"),
                Name = "Supervisor Standard Yearly",
                Description = "Standard supervisor package with yearly billing",
                UserType = UserType.Supervisor,
                Tier = PackageTier.Standard,
                BillingInterval = BillingInterval.Year,
                MonthlyPrice = 20000, // €200.00
                YearlyPrice = 200000, // €2000.00
                StripeMonthlyPriceId = "price_supervisor_standard_monthly",
                StripeYearlyPriceId = "price_supervisor_standard_yearly",
                MaxUsers = 100,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("99999999-9999-1111-1111-111111111111"),
                Name = "Supervisor Premium Monthly",
                Description = "Premium supervisor package with monthly billing",
                UserType = UserType.Supervisor,
                Tier = PackageTier.Premium,
                BillingInterval = BillingInterval.Month,
                MonthlyPrice = 30000, // €300.00
                YearlyPrice = 300000, // €3000.00
                StripeMonthlyPriceId = "price_supervisor_premium_monthly",
                StripeYearlyPriceId = "price_supervisor_premium_yearly",
                MaxUsers = 500,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubscriptionPackage
            {
                Id = Guid.Parse("99999999-9999-2222-2222-222222222222"),
                Name = "Supervisor Premium Yearly",
                Description = "Premium supervisor package with yearly billing",
                UserType = UserType.Supervisor,
                Tier = PackageTier.Premium,
                BillingInterval = BillingInterval.Year,
                MonthlyPrice = 30000, // €300.00
                YearlyPrice = 300000, // €3000.00
                StripeMonthlyPriceId = "price_supervisor_premium_monthly",
                StripeYearlyPriceId = "price_supervisor_premium_yearly",
                MaxUsers = 500,
                TrialDays = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
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

        modelBuilder.Entity<QuizType>().HasData(
            new QuizType { Id = Guid.Parse("b1b1b1b1-b1b1-1111-1111-111111111111"), Name = "text" },
            new QuizType { Id = Guid.Parse("b2b2b2b2-b2b2-2222-2222-222222222222"), Name = "imazhe" }
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