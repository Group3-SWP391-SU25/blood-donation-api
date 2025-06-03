using System.Reflection.Emit;
using BloodDonation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Infrastructures;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<BloodGroup> BloodGroups { get; set; } = null!;
    public DbSet<BloodComponent> BloodComponents { get; set; } = null!;

    public DbSet<BloodDonationRequest> BloodDonationRequests { get; set; } = null!;
    public DbSet<EmergencyBloodRequest> EmergencyBloodRequests { get; set; } = null!;
    public DbSet<BlooodStorage> BloodStorages { get; set; } = null!;
    public DbSet<HealthCheckForm> HealthCheckForms { get; set; } = null!;
    public DbSet<BloodIssue> BloodIssues { get; set; } = null!;
    public DbSet<Blog> Blogs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedData(modelBuilder);
        modelBuilder.Entity<BlooodStorage>()
            .HasOne(bs => bs.BloodGroup)
            .WithMany(bg => bg.BloodStorages)
            .HasForeignKey(bs => bs.BloodGroupId)
            .OnDelete(DeleteBehavior.Restrict); // or NoAction
        modelBuilder.ApplyConfigurationsFromAssembly(assembly: AssemblyReference.Assembly);
        modelBuilder.Entity<BloodIssue>()
            .HasOne(bi => bi.EmergencyBloodRequest)
            .WithOne(ebr => ebr.BloodIssue)
            .HasForeignKey<BloodIssue>(bi => bi.EmergencyBloodRequestId)
    .OnDelete(DeleteBehavior.NoAction); // or .NoAction
        modelBuilder.Entity<HealthCheckForm>()
            .HasOne(hcf => hcf.BloodDonationRequest)
            .WithOne(bdr => bdr.HealthCheckForm)
            .HasForeignKey<HealthCheckForm>(hcf => hcf.BloodDonateRequestId)
            .OnDelete(DeleteBehavior.NoAction); // or .NoAction
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(new Role
        {
            Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b86"),
            Name = "ADMIN",
        }, new Role
        {
            Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b85"),
            Name = "MEMBER",
        },
        new Role
        {
            Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b84"),
            Name = "NURSE",
        });
        modelBuilder.Entity<User>().HasData();
        modelBuilder.Entity<BloodGroup>().HasData(
            new BloodGroup { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b83"), Type = "A" },
            new BloodGroup { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b82"), Type = "B" },
            new BloodGroup { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b81"), Type = "O" },
            new BloodGroup { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b80"), Type = "AB" }
        );
        modelBuilder.Entity<BloodComponent>().HasData(
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b79"), Name = "Red Blood Cells" },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b78"), Name = "Plasma" },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b77"), Name = "Platelets" }
        );

        // modelBuilder.Entity<BloodUnit>().HasData();
    }
}
