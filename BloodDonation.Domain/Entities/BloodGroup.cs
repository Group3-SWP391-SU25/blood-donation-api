namespace BloodDonation.Domain.Entities;

public class BloodGroup : BaseEntity
{
    public string Type { get; set; } = string.Empty;


    #region Relationship Configurations
    public ICollection<User>? Users { get; set; } = [];
    public ICollection<BloodStorage> BloodStorages { get; set; } = [];
    #endregion
}