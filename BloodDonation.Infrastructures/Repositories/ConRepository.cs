using BloodDonation.Application.Repositories;
using BloodDonation.Domain.Entities;

namespace BloodDonation.Infrastructures.Repositories;

public class UserRepository(IServiceProvider serviceProvider)
    : GenericRepository<User>(serviceProvider), IUserRepository;
public class BloodDonationRequestRepository(IServiceProvider serviceProvider)
    : GenericRepository<BloodDonationRequest>(serviceProvider), IBloodDonationRequestRepository;
public class RoleRepository(IServiceProvider serviceProvider)
    : GenericRepository<Role>(serviceProvider), IRoleRepository;

//public class BloodUnitRepository(IServiceProvider serviceProvider)
//    : GenericRepository<BloodUnit>(serviceProvider), IBloodUnitRepository;