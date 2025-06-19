using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCGR.Domain.Entities.Transactions;

namespace SCGR.Infrastructure.Persistence.Mappings;

public sealed class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        ConfigureDataStructure(builder);
        ConfigureRelationships(builder);
    }

    private static void ConfigureDataStructure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(transaction => transaction.Id);

        builder.Property(transaction => transaction.TransactionType)
            .IsRequired();

        builder.Property(transaction => transaction.Description)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(transaction => transaction.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(transaction => transaction.TransactionDate)
            .IsRequired();
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasOne(transaction => transaction.Category) //* Cada Transaction tem uma Category
            .WithMany(category => category.Transactions) //* Uma Category pode ter muitas Transactions
            .HasForeignKey(transaction => transaction.CategoryId) //* A propriedade de chave estrangeira em Transaction é CategoryId
            .OnDelete(DeleteBehavior.Cascade); //* Quando a Category for deletada, todas as Transactions relacionadas serão excluídas
    }
}