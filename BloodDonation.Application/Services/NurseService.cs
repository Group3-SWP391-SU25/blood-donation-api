using BloodDonation.Application.Models.BloodDonations;
using BloodDonation.Application.Models.Users;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services
{
    public class NurseService : INurseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly EmailSettings emailSettings;

        public NurseService(IUnitOfWork unitOfWork, EmailSettings emailSettings)
        {
            this.unitOfWork = unitOfWork;
            this.emailSettings = emailSettings;
        }
        public async Task<object> SearchMemberAsync(
            string? searchKey,
            int pageIndex,
            int pageSize,
            Guid? bloodGroupId,
            string? address)
        {
            Expression<Func<User, bool>> filter = u =>
                (string.IsNullOrEmpty(searchKey) || u.FullName.Contains(searchKey) || u.Email.Contains(searchKey)) &&
                (!bloodGroupId.HasValue || u.BloodGroupId == bloodGroupId.Value) &&
                (string.IsNullOrEmpty(address) || u.Addresss.Contains(address)) &&
                !u.IsDeleted;

            var users = await unitOfWork.UserRepository.Search(
                filter: filter,
                includeProperties: "BloodGroup,Role,BloodRequests,BloodRequests.BloodDonation",
                pageIndex: pageIndex,
                pageSize: pageSize
            );
            // Tổng số bản ghi
            int totalRecords = await unitOfWork.UserRepository.Count(filter);

            // Tính toán phân trang
            int actualPageSize = pageSize;
            int actualPageIndex = pageIndex;
            int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);

            // Ánh xạ sang ViewModel
            var mappedData = unitOfWork.Mapper.Map<IEnumerable<MemberViewModel>>(users);

            // Trả kết quả
            return new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageIndex = actualPageIndex,
                PageSize = actualPageSize,
                Records = mappedData
            };
        }
    }
}
