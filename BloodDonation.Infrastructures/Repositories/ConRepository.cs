using BloodDonation.Application.Repositories;
using BloodDonation.Domain.Entities;

namespace BloodDonation.Infrastructures.Repositories;

public class UserRepository(IServiceProvider serviceProvider)
    : GenericRepository<User>(serviceProvider), IUserRepository;

//public class BloodUnitRepository(IServiceProvider serviceProvider)
//    : GenericRepository<BloodUnit>(serviceProvider), IBloodUnitRepository;