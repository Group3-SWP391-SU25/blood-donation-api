using BloodDonation.Application.Models.Blogs;
using BloodDonation.Domain.Entities;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IBlogService
    {
        Task<Blog> CreateAsync(BlogCreateModel model,
            CancellationToken cancellationToken = default);
        Task<object> GetAllAsync(int pageSize = 0, int pageIndex = 10,
            string? search = null,
            CancellationToken cancellationToken = default);
        Task<Blog> GetByIdAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Guid id, BlogCreateModel model,
            CancellationToken cancellationToken = default);
        Task<bool> DelAsync(Guid id);

    }
}
