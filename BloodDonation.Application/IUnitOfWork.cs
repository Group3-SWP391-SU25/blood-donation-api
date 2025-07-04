﻿using AutoMapper;
using BloodDonation.Application.Repositories;

namespace BloodDonation.Application;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IBloodDonationRequestRepository BloodDonationRequestRepository { get; }
    IRoleRepository RoleRepository { get; }
    IHealthCheckFormRepository HealthCheckFormRepository { get; }
    IBloodDonationRepository BloodDonationRepository { get; }
    IBloodStorageRepository BloodStorageRepository { get; }
    IBloodComponentRepository BloodComponentRepository { get; }
    IBloodGroupRepository BloodGroupRepository { get; }
    IBloodCheckRepository BloodCheckRepository { get; }
    IEmergencyBloodRepository EmergencyBloodRepository { get; }
    IBloodIssueRepository BloodIssueRepository { get; }

    //IBloodUnitRepository BloodUnitRepository { get; }

    IBlogRepository BlogRepository { get; }
    IMapper Mapper { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task SeedData();

}
