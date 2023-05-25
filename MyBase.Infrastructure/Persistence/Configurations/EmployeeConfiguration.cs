
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyBase.Domain.Entities;

namespace MyBase.Infrastructure.Persistence.Configurations;
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        builder.Property(x => x.Id).HasMaxLength((int)Globals.StringLength.GUID).IsRequired();
        builder.Property(at => at.Phone).HasMaxLength((int)Globals.StringLength.phoneLength);
        builder.Property(at => at.Name).HasMaxLength((int)Globals.StringLength.ShortName).IsRequired();
        builder.Property(at => at.NameEn).HasMaxLength((int)Globals.StringLength.ShortName);
        builder.Property(at => at.Image).HasMaxLength((int)Globals.StringLength.ImageLength);
        builder.Property(b => b.Deleted).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.CreatedById).HasMaxLength((int)Globals.StringLength.GUID).IsRequired();
        builder.Property(x => x.ModifiedById).HasMaxLength((int)Globals.StringLength.GUID);
        builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("CURRENT_TIMESTAMP()").IsRequired();
        builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");
        //builder.HasOne(x => x.AppUser).WithOne(e => e.Employee).HasForeignKey<Employee>(x => x.Id);
    }
}
