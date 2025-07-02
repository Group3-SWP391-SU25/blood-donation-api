using BloodDonation.Application.Models.Blogs;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Application.Utilities;
using BloodDonation.Domain.Entities;
using Ganss.Xss;
using System.Linq.Expressions;

namespace BloodDonation.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork unitOfWork;
        public BlogService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Blog> CreateAsync(BlogCreateModel model,
            CancellationToken cancellationToken = default)
        {
            var blog = unitOfWork.Mapper.Map<Blog>(model);
            var sanitizer = new HtmlSanitizer();
            blog.Content = sanitizer.Sanitize(blog.Content);
            await unitOfWork.BlogRepository.CreateAsync(blog);
            await unitOfWork.SaveChangesAsync();
            return blog;
        }

        public async Task<bool> DelAsync(Guid id)
        {
            var blog = await unitOfWork.BlogRepository.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new InvalidOperationException($"Không tìm thấy Blog với Id: {id}");
            unitOfWork.BlogRepository.SoftRemove(blog);
            return await unitOfWork.SaveChangesAsync();

        }

        public async Task<object> GetAllAsync(int pageSize = 0, int pageIndex = 10,
            string? search = null, CancellationToken cancellationToken = default)
        {
            Expression<Func<Blog, bool>> filter = x => x.IsDeleted == false;


            if (!string.IsNullOrEmpty(search))
            {
                filter.And(x => x.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase));
            }


            var pagedData = await unitOfWork.BlogRepository.Search(

                filter: filter,
                orderBy: q => q.OrderByDescending(b => b.CreatedDate),
                pageIndex: pageIndex,
                pageSize: pageSize);

            // Tổng số bản ghi
            int totalRecords = await unitOfWork.BlogRepository.Count(filter);

            // Tính toán phân trang
            int actualPageSize = pageSize;
            int actualPageIndex = pageIndex;
            int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);
            // Trả kết quả
            return new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageIndex = actualPageIndex,
                PageSize = actualPageSize,
                Records = pagedData
            };
        }

        public async Task<Blog> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await unitOfWork.BlogRepository.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new InvalidOperationException($"Không tìm thấy blog với Id {id}");
        }

        public async Task<bool> UpdateAsync(Guid id, BlogCreateModel model, CancellationToken cancellationToken = default)
        {
            var current = await unitOfWork.BlogRepository.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new InvalidOperationException($"Không tìm thấy blog với Id {id}");
            unitOfWork.Mapper.Map(model, current);
            var san = new HtmlSanitizer();
            current.Content = san.Sanitize(current.Content);
            unitOfWork.BlogRepository.Update(current);
            return await unitOfWork.SaveChangesAsync();

        }
    }
}
