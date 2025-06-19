using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodChecks
{
    public class BloodCheckCreateModel
    {
        [Required(ErrorMessage = "WBC là bắt buộc")]
        [Range(0, 100, ErrorMessage = "WBC phải từ 0 đến 100")]
        public double WBC { get; set; }

        [Required(ErrorMessage = "RBC là bắt buộc")]
        [Range(0, 100, ErrorMessage = "RBC phải từ 0 đến 100")]
        public double RBC { get; set; }

        [Required(ErrorMessage = "HGB là bắt buộc")]
        [Range(0, 500, ErrorMessage = "HGB phải từ 0 đến 500")]
        public double HGB { get; set; }

        [Required(ErrorMessage = "HCT là bắt buộc")]
        [Range(0, 200, ErrorMessage = "HCT phải từ 0 đến 200")]
        public double HCT { get; set; }

        [Required(ErrorMessage = "MCV là bắt buộc")]
        [Range(0, 200, ErrorMessage = "MCV phải từ 0 đến 200")]
        public double MCV { get; set; }

        [Required(ErrorMessage = "MCH là bắt buộc")]
        [Range(0, 200, ErrorMessage = "MCH phải từ 0 đến 200")]
        public double MCH { get; set; }

        [Required(ErrorMessage = "MCHC là bắt buộc")]
        [Range(0, 200, ErrorMessage = "MCHC phải từ 0 đến 200")]
        public double MCHC { get; set; }

        [Required(ErrorMessage = "PLT là bắt buộc")]
        [Range(0, 1000, ErrorMessage = "PLT phải từ 0 đến 1000")]
        public double PLT { get; set; }

        [Required(ErrorMessage = "MPV là bắt buộc")]
        [Range(0, 100, ErrorMessage = "MPV phải từ 0 đến 100")]
        public double MPV { get; set; }

        [MaxLength(1000, ErrorMessage = "Mô tả không được vượt quá 1000 ký tự")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "BloodGroupId là bắt buộc")]
        public Guid BloodGroupId { get; set; }

        [Required(ErrorMessage = "BloodDonationId là bắt buộc")]
        public Guid BloodDonationId { get; set; }
    }
}
