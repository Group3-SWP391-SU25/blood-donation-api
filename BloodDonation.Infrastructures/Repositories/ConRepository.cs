using BloodDonation.Application.Repositories;
using BloodDonation.Domain.Entities;

namespace BloodDonation.Infrastructures.Repositories;

public class UserRepository(IServiceProvider serviceProvider)
    : GenericRepository<User>(serviceProvider), IUserRepository;
public class BloodDonationRequestRepository(IServiceProvider serviceProvider)
    : GenericRepository<BloodDonationRequest>(serviceProvider), IBloodDonationRequestRepository;
public class RoleRepository(IServiceProvider serviceProvider)
    : GenericRepository<Role>(serviceProvider), IRoleRepository;
public class HealthCheckFormRepository(IServiceProvider serviceProvider)
    : GenericRepository<HealthCheckForm>(serviceProvider), IHealthCheckFormRepository;
public class BloodDonationRepository(IServiceProvider serviceProvider)
    : GenericRepository<BloodDonation.Domain.Entities.BloodDonation>(serviceProvider), IBloodDonationRepository;
public class BloodStorageRepository(IServiceProvider serviceProvider)
    : GenericRepository<BloodStorage>(serviceProvider), IBloodStorageRepository;
public class BloodComponentRepository(IServiceProvider serviceProvider)
    : GenericRepository<BloodComponent>(serviceProvider), IBloodComponentRepository;

//public class BloodUnitRepository(IServiceProvider serviceProvider)
//    : GenericRepository<BloodUnit>(serviceProvider), IBloodUnitRepository;