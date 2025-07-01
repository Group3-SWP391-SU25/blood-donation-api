using BloodDonation.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
