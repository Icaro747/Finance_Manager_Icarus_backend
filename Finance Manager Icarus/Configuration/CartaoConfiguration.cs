using Finance_Manager_Icarus.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Finance_Manager_Icarus.Configuration;

public class CartaoConfiguration : IEntityTypeConfiguration<Cartao>
{
    public void Configure(EntityTypeBuilder<Cartao> builder)
    {
        builder.HasKey(e => e.Cartao_Id);
        builder.Property(e => e.Nome).IsRequired();
        builder.Property(e => e.Numero).IsRequired();

        builder.HasOne(x => x.Banco)
            .WithMany(x => x.Cartoes)
            .HasForeignKey(x => x.Banco_Id);
    }
}
