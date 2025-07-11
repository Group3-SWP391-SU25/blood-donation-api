﻿using BloodDonation.Application.Models.Nurse;
using BloodDonation.Application.Models.Supervisor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<NurseDashboard> GetNurseDashboardAsync();
        Task<SupervisorDashboardViewModel> GetSupervisorDashboard();
    }
}
