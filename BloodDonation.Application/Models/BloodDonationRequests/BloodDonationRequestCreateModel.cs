using AutoMapper.Configuration.Annotations;
using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodDonationRequests
{
    public class BloodDonationRequestCreateModel
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "BloodType là bắt buộc")]
        [EnumDataType(typeof(BloodTypeEnum), ErrorMessage = "BloodType không hợp lệ")]
        public BloodTypeEnum BloodType { get; set; } = BloodTypeEnum.Unknown;
        [JsonIgnore]
        public BloodDonationRequestStatus Status { get; set; } = BloodDonationRequestStatus.Pending;
        public string ReasonReject { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ngày yêu cầu hiến máu không được để trống")]
        public DateTime DonatedDateRequest { get; set; }
        [Required(ErrorMessage = "TimeSlot là bắt buộc")]
        [EnumDataType(typeof(TimeSlotEnum), ErrorMessage = "TimeSlot không hợp lệ")]
        public TimeSlotEnum TimeSlot { get; set; }
        [Required(ErrorMessage = "Bạn phải cung cấp thông tin về lịch sử truyền máu")]
        public bool HasBloodTransfusionHistory { get; set; }
        [Required(ErrorMessage = "Bạn phải cung cấp thông tin về bệnh tật hoặc thuốc gần đây")]
        public bool HasRecentIllnessOrMedication { get; set; }
        [Required(ErrorMessage = "Bạn phải cung cấp thông tin về phẫu thuật hoặc thủ thuật gần đây")]
        public bool HasRecentSkinPenetrationOrSurgery { get; set; }
        [Required(ErrorMessage = "Bạn phải cung cấp thông tin về việc tiêm chích ma túy")]
        public bool HasDrugInjectionHistory { get; set; }
        [Required(ErrorMessage = "Bạn phải cung cấp thông tin về việc đã từng đến khu vực dịch bệnh")]
        public bool HasVisitedEpidemicArea { get; set; }
    }
}
