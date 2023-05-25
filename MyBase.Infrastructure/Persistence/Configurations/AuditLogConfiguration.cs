using Base6.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyBase.Domain.Entities;

namespace MyBase.Infrastructure.Persistence.Configurations;
public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");
        builder.Property(a => a.Id).HasMaxLength((int)Globals.StringLength.GUID)
            .IsRequired();

        builder.Property(x => x.Action)
                .HasMaxLength((int)Globals.StringLength.ShortName)
            .IsRequired();


        builder.Property(x => x.TableName)
                .HasMaxLength((int)Globals.StringLength.ShortName)
            .IsRequired();

        builder.Property(x => x.Feature)
                .HasMaxLength((int)Globals.StringLength.ShortName)
            .IsRequired();





        builder.Property(b => b.CreationDate).HasColumnType("DATETIME")
            .HasDefaultValueSql("CURRENT_TIMESTAMP()").IsRequired();


        builder.Property(x => x.AppUserId)
            .IsRequired();

        //builder.HasOne(x => x.AppUser)
        //    .WithMany(x => x.AuditLogs)
        //    .HasForeignKey(x => x.AppUserId);


    }
}
