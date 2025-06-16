using BloodDonation.Application.Models.BloodComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IBloodComponentService
    {
        Task<IEnumerable<BloodComponentViewModel>> GetAllAsync(string? search = null);
        //Task<BloodComponent> GetByIdAsync(int id);
        //Task<BloodComponent> CreateAsync(BloodComponent bloodComponent);
        //Task<BloodComponent> UpdateAsync(BloodComponent bloodComponent);
        //Task<bool> DeleteAsync(int id);
    }
}
