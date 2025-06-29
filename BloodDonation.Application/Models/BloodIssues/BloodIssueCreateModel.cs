using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodIssues
{
    public class BloodIssueCreateModel
    {
        [Required(ErrorMessage = "Danh sách kho máu không được để trống.")]
        public List<Guid> BloodStorageIds { get; set; } = new List<Guid>();
    }
}
