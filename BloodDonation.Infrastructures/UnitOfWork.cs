using AutoMapper;
using BloodDonation.Application;
using BloodDonation.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Infrastructures;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext dbContext;
    public UnitOfWork(IServiceProvider serviceProvider)
    {
        Mapper = serviceProvider.GetRequiredService<IMapper>();
        dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        UserRepository = serviceProvider.GetRequiredService<IUserRepository>();
        BloodDonationRequestRepository = serviceProvider.GetRequiredService<IBloodDonationRequestRepository>();
        RoleRepository = serviceProvider.GetRequiredService<IRoleRepository>();
        HealthCheckFormRepository = serviceProvider.GetRequiredService<IHealthCheckFormRepository>();
        BloodDonationRepository = serviceProvider.GetRequiredService<IBloodDonationRepository>();
        BloodStorageRepository = serviceProvider.GetRequiredService<IBloodStorageRepository>();
        BloodComponentRepository = serviceProvider.GetRequiredService<IBloodComponentRepository>();
        BloodGroupRepository = serviceProvider.GetRequiredService<IBloodGroupRepository>();
        BloodCheckRepository = serviceProvider.GetRequiredService<IBloodCheckRepository>();
        EmergencyBloodRepository = serviceProvider.GetRequiredService<IEmergencyBloodRepository>();
        BloodIssueRepository = serviceProvider.GetRequiredService<IBloodIssueRepository>();
        // BloodUnitRepository = serviceProvider.GetRequiredService<IBloodUnitRepository>();
        BlogRepository = serviceProvider.GetRequiredService<IBlogRepository>();

    }
    public IBlogRepository BlogRepository { get; }
    public IUserRepository UserRepository { get; }
    public IBloodDonationRequestRepository BloodDonationRequestRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public IHealthCheckFormRepository HealthCheckFormRepository { get; }
    public IBloodDonationRepository BloodDonationRepository { get; }
    public IBloodStorageRepository BloodStorageRepository { get; }
    public IBloodComponentRepository BloodComponentRepository { get; }
    public IBloodGroupRepository BloodGroupRepository { get; }
    public IBloodCheckRepository BloodCheckRepository { get; }
    public IEmergencyBloodRepository EmergencyBloodRepository { get; }
    public IBloodIssueRepository BloodIssueRepository { get; }
    // public IBloodUnitRepository BloodUnitRepository { get; }

    public IMapper Mapper { get; }


    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task SeedData()
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
