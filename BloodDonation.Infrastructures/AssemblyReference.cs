using System.Reflection;

namespace BloodDonation.Infrastructures;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}