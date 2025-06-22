namespace BloodDonation.Domain.Enums;

public enum UserStatusEnum
{
    Active, InActive
}

//public enum BloodTypeEnum //có 8 loại nhóm máu ở người
//{
//    APlus, AMinus, BPlus, BMinus, ABPlus, ABMinus, OPlus, OMinus
//}

//public enum BloodDonationStatusEnum
//{
//    Pending, Approved, Rejected
//}

public enum BloodDonationPurposeEnum
{
    Normal, Emergency, Research
}

public enum BloodDonationLocationEnum
{
    Hospital, Clinic, School, Company, Other
}

public enum BloodDonationTypeEnum //máu toàn phần, huyết tương, tiểu cầu, hồng cầu
{
    WholeBlood, Plasma, Platelet, RedBloodCell
}

public enum BloodDonationMethodEnum
{
    Direct, Indirect
}

public enum BloodDonationRequestStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2,
    Cancelled = 3
}

public enum TimeSlotEnum
{
    Slot7hto7h30 = 0,
    Slot7h30to8h = 1,
    Slot8hto8h30 = 2,
    Slot8h30to9h = 3,
    Slot9hto9h30 = 4,
    Slot0h30to10h = 5,
    Slot10hto10h30 = 6,
    Slot13h30to14h = 7,
    Slot14hto14h30 = 8,
    Slot14h30to15h = 9,
    Slot15hto15h30 = 10,
    Slot15h30to16h = 11,
}

public enum BloodTypeEnum
{
    A = 0,
    B = 1,
    AB = 2,
    O = 3,
    Unknown = 4
}

public enum BloodDonationStatusEnum
{
    InProgress = 0,
    Donated = 1,
    Cancelled = 2,
    Checked = 3,
}

public enum BloodStorageStatusEnum
{
    UnChecked = 0,
    Safe = 1,
    Expired = 2,
    Exported = 3,
    Dangerous = 4,
    Locked = 5
}
public class BloodComponentType
{
    public static readonly BloodComponentType WholeBlood =
        new("Máu toàn phần", Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b63"));

    public string Name { get; }
    public Guid Id { get; }

    private BloodComponentType(string name, Guid id)
    {
        Name = name;
        Id = id;
    }
}
