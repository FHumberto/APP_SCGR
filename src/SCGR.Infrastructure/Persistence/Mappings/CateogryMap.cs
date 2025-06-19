using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCGR.Domain.Entities.Categories;

namespace SCGR.Infrastructure.Persistence.Mappings;

public sealed class CateogryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        ConfigureDataStructure(builder);
        ConfigureIndexes(builder);
    }

    private static void ConfigureDataStructure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(100);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Category> builder)
    {
        builder.HasIndex(category => category.Name)
            .IsUnique();
    }
}
