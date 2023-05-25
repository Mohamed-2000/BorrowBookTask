using Base6.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBase.Domain.Entities;

namespace Base6.Infrastructure.Persistence.Configurations;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasMaxLength((int)Globals.StringLength.GUID).IsRequired();
        builder.Property(x => x.UserName).HasMaxLength((int)Globals.StringLength.GUID).IsRequired();
        builder.Property(t => t.Name).HasMaxLength((int)Globals.StringLength.ShortName).IsRequired();
        builder.Property(x => x.NormalizedUserName).HasMaxLength((int)Globals.StringLength.GUID).IsRequired();
        builder.Property(a => a.Email).HasMaxLength((int)Globals.StringLength.EmailLength);
        builder.Property(a => a.NormalizedEmail).HasMaxLength((int)Globals.StringLength.EmailLength);
        builder.Property(b => b.EmailConfirmed).HasDefaultValue(false).IsRequired();
        builder.Property(a => a.PhoneNumber).HasMaxLength((int)Globals.StringLength.phoneLength);
        builder.Property(b => b.PhoneNumberConfirmed).HasDefaultValue(false).IsRequired();
        builder.Property(b => b.TwoFactorEnabled).HasDefaultValue(false).IsRequired();
        builder.Property(b => b.LockoutEnabled).HasDefaultValue(false).IsRequired();
        builder.Property(a => a.IdentityNo).HasMaxLength((int)Globals.StringLength.LabelLenght).IsRequired();
        builder.Property(b => b.PasswordHash).HasMaxLength((int)Globals.StringLength.HashLenght).IsRequired();
        builder.Property(b => b.SecurityStamp).HasMaxLength((int)Globals.StringLength.HashLenght).IsRequired();
        builder.Property(b => b.ConcurrencyStamp).HasMaxLength((int)Globals.StringLength.HashLenght);
        builder.HasIndex(a => a.Email).IsUnique();
        builder.HasIndex(a => a.IdentityNo).IsUnique();
        builder.Property(b => b.Deleted).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.CreatedById).HasMaxLength((int)Globals.StringLength.GUID).IsRequired();
        builder.Property(x => x.ModifiedById).HasMaxLength((int)Globals.StringLength.GUID);
        builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("CURRENT_TIMESTAMP()").IsRequired();
        builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");
        builder.Property(b => b.LockoutEnd).HasColumnType("DATETIME");
    }
}
