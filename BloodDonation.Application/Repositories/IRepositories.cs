using BloodDonation.Domain.Entities;

namespace BloodDonation.Application.Repositories;

public interface IUserRepository : IGenericRepository<User>;
public interface IBloodDonationRequestRepository : IGenericRepository<BloodDonationRequest>;
//public interface IBloodUnitRepository : IGenericRepository<BloodUnit>;
