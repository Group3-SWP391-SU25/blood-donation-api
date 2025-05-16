using AutoMapper;
using BloodDonation.Application.Repositories;

namespace BloodDonation.Application;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    //IBloodUnitRepository BloodUnitRepository { get; }
    IMapper Mapper { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task SeedData();

}
