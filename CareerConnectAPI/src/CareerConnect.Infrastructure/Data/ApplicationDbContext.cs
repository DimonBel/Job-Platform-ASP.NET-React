using CareerConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ApplicationEntity = CareerConnect.Domain.Entities.Application;

namespace CareerConnect.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<JobTag> JobTags => Set<JobTag>();
    public DbSet<JobResponsibility> JobResponsibilities => Set<JobResponsibility>();
    public DbSet<JobRequirement> JobRequirements => Set<JobRequirement>();
    public DbSet<JobBenefit> JobBenefits => Set<JobBenefit>();
    public DbSet<User> Users => Set<User>();
    public DbSet<ApplicationEntity> Applications => Set<ApplicationEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Job configuration
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Location).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Salary).HasMaxLength(100);
            entity.Property(e => e.JobType).HasMaxLength(50);
            entity.Property(e => e.ExperienceLevel).HasMaxLength(50);
            
            entity.HasOne(e => e.Company)
                .WithMany(c => c.Jobs)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Category)
                .WithMany(c => c.Jobs)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.IsFeatured);
            entity.HasIndex(e => e.IsActive);
            entity.HasIndex(e => e.PostedDate);
        });

        // Company configuration
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Industry).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Rating).HasPrecision(3, 2);
            
            entity.HasIndex(e => e.Name);
        });

        // Category configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Tag configuration
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // JobTag (Many-to-Many) configuration
        modelBuilder.Entity<JobTag>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.TagId });

            entity.HasOne(e => e.Job)
                .WithMany(j => j.JobTags)
                .HasForeignKey(e => e.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Tag)
                .WithMany(t => t.JobTags)
                .HasForeignKey(e => e.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // JobResponsibility configuration
        modelBuilder.Entity<JobResponsibility>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            
            entity.HasOne(e => e.Job)
                .WithMany(j => j.Responsibilities)
                .HasForeignKey(e => e.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // JobRequirement configuration
        modelBuilder.Entity<JobRequirement>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            
            entity.HasOne(e => e.Job)
                .WithMany(j => j.Requirements)
                .HasForeignKey(e => e.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // JobBenefit configuration
        modelBuilder.Entity<JobBenefit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            
            entity.HasOne(e => e.Job)
                .WithMany(j => j.Benefits)
                .HasForeignKey(e => e.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(20);
            
            entity.HasIndex(e => e.Email).IsUnique();
            
            entity.HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Application configuration
        modelBuilder.Entity<ApplicationEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasMaxLength(20);
            
            entity.HasOne(e => e.Job)
                .WithMany(j => j.Applications)
                .HasForeignKey(e => e.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Applications)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.JobId, e.UserId }).IsUnique();
        });
    }
}
