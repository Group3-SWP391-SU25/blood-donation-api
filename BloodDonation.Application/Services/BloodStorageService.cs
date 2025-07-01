using BloodDonation.Application.Models.BloodStorage;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System.Linq.Expressions;

namespace BloodDonation.Application.Services
{
    public class BloodStorageService : IBloodStorageService
    {
        private readonly IUnitOfWork unitOfWork;
        public BloodStorageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        //search
        public async Task<object> SearchAsync(Guid? bloodGroupId, Guid? componentId, int pageIndex, int pageSize, string? search = "", BloodStorageStatusEnum? status = null, CancellationToken cancellationToken = default)
        {
            // Biểu thức lọc
            Expression<Func<BloodStorage, bool>> filter = b =>
                (!status.HasValue || b.Status == status) &&
                (bloodGroupId == null || b.BloodGroupId == bloodGroupId) &&
                (componentId == null || b.BloodComponentId == componentId) &&
                (string.IsNullOrEmpty(search) || b.Code!.Contains(search) || b.BloodDonate.BloodDonationRequest.User.FullName.Contains(search));

            // Truy vấn dữ liệu đã lọc, phân trang
            var pagedData = await unitOfWork.BloodStorageRepository.Search(
                filter: filter,
                includeProperties: "BloodDonate,BloodComponent,BloodGroup,BloodDonate.BloodDonationRequest,BloodDonate.BloodDonationRequest.User,BloodDonate.BloodCheck",
                orderBy: q => q.OrderByDescending(b => b.CreatedDate),
                pageIndex: pageIndex,
                pageSize: pageSize);

            // Tổng số bản ghi
            int totalRecords = await unitOfWork.BloodStorageRepository.Count(filter);

            // Tính toán phân trang
            int actualPageSize = pageSize;
            int actualPageIndex = pageIndex;
            int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);

            // Ánh xạ sang ViewModel
            var mappedData = unitOfWork.Mapper.Map<List<BloodStorageViewModel>>(pagedData);

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
        public async Task ExpireOutdatedBloodAsync()
        {
            var now = DateTime.Now;

            var expiredBloods = await unitOfWork.BloodStorageRepository
                .Search(b => b.Status != BloodStorageStatusEnum.Expired && b.ExpiredDate < now);

            foreach (var blood in expiredBloods)
            {
                blood.Status = BloodStorageStatusEnum.Expired;
            }
            if (expiredBloods.Any())
            {
                unitOfWork.BloodStorageRepository.UpdateRange((List<BloodStorage>)expiredBloods);
                await unitOfWork.SaveChangesAsync();
            }
        }
        public async Task<object> GetAvailableBloods(int pageIndex, int pageSize, BloodStorageStatusEnum? status = null, Guid? BloodComponentId = null, Guid? BloodGroupId = null, int? volume = null, CancellationToken cancellationToken = default)
        {
            // Biểu thức lọc
            Expression<Func<BloodStorage, bool>> filter = b =>
                (status == null || b.Status == status) &&
                (string.IsNullOrEmpty(BloodComponentId.ToString()) || b.BloodComponentId == BloodComponentId) &&
                (string.IsNullOrEmpty(BloodGroupId.ToString()) || b.BloodGroupId == BloodGroupId) &&
                (volume == null || b.Volume == volume);

            // Truy vấn dữ liệu đã lọc, phân trang
            var pagedData = await unitOfWork.BloodStorageRepository.Search(
                filter: filter,
                includeProperties: "BloodComponent,BloodGroup",
                orderBy: q => q.OrderByDescending(b => b.CreatedDate),
                pageIndex: pageIndex,
                pageSize: pageSize);

            // Tổng số bản ghi
            int totalRecords = await unitOfWork.BloodStorageRepository.Count(filter);

            // Tính toán phân trang
            int actualPageSize = pageSize;
            int actualPageIndex = pageIndex;
            int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);

            // Ánh xạ sang ViewModel
            var mappedData = unitOfWork.Mapper.Map<List<BloodStorageViewModel>>(pagedData);

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
        public async Task PrepareBloodAsync(Guid id, BloodStorageCreateModel dto)
        {
            Guid wholeBloodId = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b63"); // ID of Whole Blood component
            //validate
            var existingBlood = await unitOfWork.BloodStorageRepository.GetByCondition(b => b.Id == id);
            if (existingBlood == null)
            {
                throw new Exception("Không tìm thấy máu.");
            }
            if (existingBlood.BloodComponentId != wholeBloodId)
            {
                throw new Exception("Chỉ có thể điều chế bị máu toàn phần.");
            }
            if (existingBlood.Volume < dto.Volume)
            {
                throw new Exception("Khối lượng máu sản phẩm không thể lớn hơn khối lương máu điều chế.");
            }
            if (existingBlood.Volume <= 0)
            {
                throw new Exception("Khối lượng máu phải lớn hơn 0.");
            }

            //Create BloodStorage entity from DTO

            // Generate new code for BloodStorage
            var existingStorange = await unitOfWork.BloodStorageRepository.Search(x => x.Code != null && x.Code.StartsWith("BS"));

            int maxNumericCode = 0;
            foreach (var code in existingStorange)
            {
                var numericPart = code.Code!.Substring(2); // Remove "BS"
                if (int.TryParse(numericPart, out int num))
                {
                    maxNumericCode = Math.Max(maxNumericCode, num);
                }
            }

            var nextCode = $"BS{(maxNumericCode + 1).ToString("D5")}"; // BS00001, BS00002, ...

            var component = await unitOfWork.BloodComponentRepository.GetByCondition(b => b.Id == dto.BloodComponentId);

            var newbloodStorage = new BloodStorage();
            newbloodStorage.Code = nextCode; // Assign new code
            newbloodStorage.BloodComponentId = dto.BloodComponentId;
            newbloodStorage.Volume = dto.Volume;
            newbloodStorage.BloodGroupId = existingBlood.BloodGroupId;
            newbloodStorage.BloodDonationId = existingBlood.BloodDonationId;
            newbloodStorage.Status = existingBlood.Status;
            newbloodStorage.ExpiredDate = DateTime.Now.AddDays(component.ShelfLifeInDay);
            //Update existing blood storage
            existingBlood.Volume = 0;

            //Save changes
            await unitOfWork.BloodStorageRepository.CreateAsync(newbloodStorage);
            unitOfWork.BloodStorageRepository.Update(existingBlood);
            await unitOfWork.SaveChangesAsync();
        }
        public async Task<object> VolumeSummary(Guid? bloodGroupId, Guid? componentId)
        {
            // Filter chung
            Expression<Func<BloodStorage, bool>> filter = b =>
                b.Volume > 0 &&
                (bloodGroupId == null || b.BloodGroupId == bloodGroupId) &&
                (componentId == null || b.BloodComponentId == componentId);
            // Lấy danh sách đã lọc
            var bloodList = await unitOfWork.BloodStorageRepository.Search(filter);

            // Tính tổng các loại ml theo Status
            var totalVolume = bloodList.Sum(b => b.Volume);

            var safeVolume = bloodList
                .Where(b => b.Status == BloodStorageStatusEnum.Safe)
                .Sum(b => b.Volume);

            var dangerVolume = bloodList
                .Where(b => b.Status == BloodStorageStatusEnum.Dangerous)
                .Sum(b => b.Volume);

            var expiredVolume = bloodList
                .Where(b => b.Status == BloodStorageStatusEnum.Expired)
                .Sum(b => b.Volume);

            // Trả kết quả
            return new
            {
                TotalVolume = totalVolume,
                SafeVolume = safeVolume,
                WarningVolume = dangerVolume,
                ExpiredVolume = expiredVolume
            };
        }

    }
}
