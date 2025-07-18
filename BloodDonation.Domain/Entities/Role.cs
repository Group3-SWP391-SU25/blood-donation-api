namespace BloodDonation.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    #region Relationship Configuration
    public virtual ICollection<User> Users { get; set; } = [];
    #endregion
}
public static class RoleNames
{
    public const string SUPER_VISOR = "SUPERVISOR";
    public const string ADMIN = "ADMIN";
    public const string MEMBER = "MEMBER";
    public const string NURSE = "NURSE";
}