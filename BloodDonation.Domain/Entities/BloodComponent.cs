using System.Collections.Generic;

namespace BloodDonation.Domain.Entities;

public class BloodComponent : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public double StorageTemerature { get; set; }
    public int ShelfLifeInDay { get; set; }
    public string Status { get; set; } = string.Empty;

    #region Relationship Configuration
    public virtual ICollection<BlooodStorage> BloodStorages { get; set; } = new List<BlooodStorage>();
        
    #endregion
}