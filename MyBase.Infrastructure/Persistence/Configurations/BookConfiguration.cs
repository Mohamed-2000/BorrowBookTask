using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBase.Domain.Entities;

namespace MyBase.Infrastructure.Persistence.Configurations;
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");
        builder.Property(x => x.Id).HasMaxLength((int)Globals.StringLength.GUID).IsRequired();
        builder.Property(at => at.Name).HasMaxLength((int)Globals.StringLength.ShortName).IsRequired();
        builder.Property(at => at.PictureUrl).HasMaxLength((int)Globals.StringLength.ImageLength);
        builder.Property(b => b.Deleted).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.CreatedById).HasMaxLength((int)Globals.StringLength.GUID).IsRequired();
        builder.Property(x => x.ModifiedById).HasMaxLength((int)Globals.StringLength.GUID);
        builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("CURRENT_TIMESTAMP()").IsRequired();
        builder.Property(b => b.ModificationDate).HasColumnType("DATETIME");
       
    }
}
