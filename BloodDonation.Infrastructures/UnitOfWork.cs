﻿using System;
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
        dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        UserRepository = serviceProvider.GetRequiredService<IUserRepository>();
        // BloodUnitRepository = serviceProvider.GetRequiredService<IBloodUnitRepository>();
 
    }
    public IUserRepository UserRepository { get; }
    // public IBloodUnitRepository BloodUnitRepository { get; }

    public IMapper Mapper { get; }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task SeedData()
    {
        throw new NotImplementedException();
    }
}
