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
    public DbSet<BloodStorage> BloodStorages { get; set; } = null!;
    public DbSet<HealthCheckForm> HealthCheckForms { get; set; } = null!;
    public DbSet<BloodIssue> BloodIssues { get; set; } = null!;
    public DbSet<Blog> Blogs { get; set; } = null!;
    public DbSet<BloodCheck> BloodChecks { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedData(modelBuilder);
        modelBuilder.Entity<BloodStorage>()
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
        modelBuilder.Entity<BloodDonation.Domain.Entities.BloodDonation>()
            .HasOne(x => x.BloodDonationRequest).WithOne(x => x.BloodDonation)
            .HasForeignKey<BloodDonation.Domain.Entities.BloodDonation>(x => x.BloodDonationRequestId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<BloodDonation.Domain.Entities.BloodDonation>()
            .HasOne(x => x.BloodStorage).WithOne(x => x.BloodDonate)
            .HasForeignKey<BloodStorage>(x => x.BloodDonationId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<BloodCheck>()
            .HasOne(bc => bc.BloodDonation)
            .WithOne(bd => bd.BloodCheck)
            .HasForeignKey<BloodCheck>(bc => bc.BloodDonationId)
            .OnDelete(DeleteBehavior.NoAction); // or .NoAction

        modelBuilder.Entity<BloodCheck>()
            .HasOne(bc => bc.BloodGroup)
            .WithMany(bg => bg.BloodChecks)
            .HasForeignKey(bc => bc.BloodGroupId)
            .OnDelete(DeleteBehavior.Restrict); // or .NoAction
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
            new BloodGroup { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b83"), Type = "A", RhFactor = "+" },
            new BloodGroup { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b82"), Type = "A", RhFactor = "-" },
            new BloodGroup { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b81"), Type = "B", RhFactor = "+" },
            new BloodGroup { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b80"), Type = "B", RhFactor = "-" },
            new BloodGroup { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b7f"), Type = "AB", RhFactor = "+" },
            new BloodGroup { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b7e"), Type = "AB", RhFactor = "-" },
            new BloodGroup { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b7d"), Type = "O", RhFactor = "+" },
            new BloodGroup { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b7c"), Type = "O", RhFactor = "-" }
        );
        modelBuilder.Entity<BloodComponent>().HasData(
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b79"), Name = "Hồng cầu lắng", MinStorageTemerature = 2, MaxStorageTemerature = 6, ShelfLifeInDay = 35 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b78"), Name = "Khối hồng cầu có dung dịch bảo quản", MinStorageTemerature = 2, MaxStorageTemerature = 6, ShelfLifeInDay = 35 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b77"), Name = "Khối hồng cầu rửa (xử lý trong hệ thống hở)", MinStorageTemerature = 2, MaxStorageTemerature = 6, ShelfLifeInDay = 1 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b76"), Name = "Khối hồng cầu rửa (rửa trong hệ thống kín và có bổ sung dung dịch bảo quản hồng cầu)", MinStorageTemerature = 2, MaxStorageTemerature = 6, ShelfLifeInDay = 14 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b75"), Name = "Khối hồng cầu đông lạnh", MinStorageTemerature = -80, MaxStorageTemerature = -60, ShelfLifeInDay = 365242199 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b74"), Name = "Khối tiểu cầu (Xử lí kín)", MinStorageTemerature = 20, MaxStorageTemerature = 24, ShelfLifeInDay = 5 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b73"), Name = "Khối tiểu cầu (Xử lí hở)", MinStorageTemerature = 20, MaxStorageTemerature = 24, ShelfLifeInDay = 0.25 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b72"), Name = "Khối tiểu cầu lọc bạch cầu (Xử lí kín)", MinStorageTemerature = 20, MaxStorageTemerature = 24, ShelfLifeInDay = 5 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b71"), Name = "Khối tiểu cầu lọc bạch cầu (Xử lí hở)", MinStorageTemerature = 20, MaxStorageTemerature = 24, ShelfLifeInDay = 0.25 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b70"), Name = "Huyết tương đông lạnh", MinStorageTemerature = -272, MaxStorageTemerature = -25, ShelfLifeInDay = 2 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b69"), Name = "Huyết tương (Xử lí kín)", MinStorageTemerature = 2, MaxStorageTemerature = 6, ShelfLifeInDay = 14 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b68"), Name = "Huyết tương (Xử lí hở)", MinStorageTemerature = 2, MaxStorageTemerature = 6, ShelfLifeInDay = 1 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b67"), Name = "Tủa lạnh (Xử lí kín)", MinStorageTemerature = 2, MaxStorageTemerature = 6, ShelfLifeInDay = 14 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b66"), Name = "Tủa lạnh (Xử lí hở)", MinStorageTemerature = 2, MaxStorageTemerature = 6, ShelfLifeInDay = 1 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b65"), Name = "Khối bạch cầu hạt trung tính", MinStorageTemerature = 20, MaxStorageTemerature = 24, ShelfLifeInDay = 1 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b64"), Name = "Máu toàn phần (Bảo quản)", MinStorageTemerature = 2, MaxStorageTemerature = 6, ShelfLifeInDay = 35 },
            new BloodComponent { Id = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b63"), Name = "Máu toàn phần", MinStorageTemerature = 20, MaxStorageTemerature = 24, ShelfLifeInDay = 1 }
        );

        // modelBuilder.Entity<BloodUnit>().HasData();
    }
}
