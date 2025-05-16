namespace BloodDonation.Domain.Enums;

public enum UserStatusEnum
{
    Active, InActive
}

public enum BloodTypeEnum //có 8 loại nhóm máu ở người
{
    APlus, AMinus, BPlus, BMinus, ABPlus, ABMinus, OPlus, OMinus
}

public enum BloodDonationStatusEnum
{
    Pending, Approved, Rejected
}

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