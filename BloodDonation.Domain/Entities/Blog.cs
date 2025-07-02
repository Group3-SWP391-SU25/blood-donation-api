using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonation.Domain.Entities;

public class Blog : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    [Column(TypeName = "nvarchar(max)")]
    public string Content { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;

}