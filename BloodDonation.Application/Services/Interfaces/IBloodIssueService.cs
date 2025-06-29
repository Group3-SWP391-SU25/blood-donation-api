using BloodDonation.Application.Models.BloodIssues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IBloodIssueService
    {
        Task<bool> CreateBloodIssueAsync(Guid emergencyBloodRequestId, BloodIssueCreateModel reqDto);
    }
}
