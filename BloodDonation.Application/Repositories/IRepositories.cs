using BloodDonation.Domain.Entities;

namespace BloodDonation.Application.Repositories;

public interface IUserRepository : IGenericRepository<User>;
public interface IRoleRepository : IGenericRepository<Role>;
public interface IBloodDonationRequestRepository : IGenericRepository<BloodDonationRequest>;
public interface IHealthCheckFormRepository : IGenericRepository<HealthCheckForm>;
public interface IBloodDonationRepository : IGenericRepository<BloodDonation.Domain.Entities.BloodDonation>;
public interface IBloodStorageRepository : IGenericRepository<BloodStorage>;
public interface IBloodComponentRepository : IGenericRepository<BloodComponent>;
//public interface IBloodUnitRepository : IGenericRepository<BloodUnit>;
