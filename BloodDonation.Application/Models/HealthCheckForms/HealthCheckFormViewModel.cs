using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.HealthCheckForms
{
    public class HealthCheckFormViewModel : BaseModel
    {
        [Range(1, 99, ErrorMessage = "Tuổi phải từ 1 đến 99.")]
        public int Age { get; set; }
        public double Weight { get; set; }
        [Range(250, 500, ErrorMessage = "Lượng máu hiến phải từ 250ml đến 500ml.")]
        public double VolumeBloodDonated { get; set; }
        [Range(0, 200, ErrorMessage = "Chỉ số hemoglobin phải từ 0 đến 200 g/l.")]
        public double Hemoglobin { get; set; }
        public bool IsInfectiousDisease { get; set; }
        public bool IsPregnant { get; set; }
        public bool IsUsedAlcoholRecently { get; set; }
        public bool HasChronicDisease { get; set; }
        public bool HasUnsafeSexualBehaviourOrSameSexSexualContact { get; set; }
        [MaxLength(1000, ErrorMessage = "Ghi chú không được vượt quá 1000 ký tự.")]
        public string Note { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phải cung cấp Id yêu cầu hiến máu.")]
        public Guid BloodDonateRequestId { get; set; }
        //public BloodDonationRequests.BloodDonationRequestViewModel BloodDonationRequest { get; set; }
    }
}
