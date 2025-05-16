using BloodDonation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Infrastructures.FluentApis;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //builder.HasKey(x => x.Id);
        //builder.HasIndex(x => x.PhoneNumber);
        ///*.IsUnique(true);*/
        //builder.HasIndex(x => x.Email)
        //    .IsUnique(true);
        //builder.HasMany(x => x.Request)
        //    .WithOne(x => x.User)
        //    .HasForeignKey(x => x.UserId);
    }
}