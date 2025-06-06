using System;
using System.Collections.Generic;
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
        // BloodUnitRepository = serviceProvider.GetRequiredService<IBloodUnitRepository>();

    }
    public IUserRepository UserRepository { get; }
    public IBloodDonationRequestRepository BloodDonationRequestRepository { get; }
    public IRoleRepository RoleRepository { get; }
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
