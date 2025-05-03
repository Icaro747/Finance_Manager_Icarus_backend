using Finance_Manager_Icarus.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Finance_Manager_Icarus.Configuration;

public class MovimentacaoConfiguration : IEntityTypeConfiguration<Movimentacao>
{
    public void Configure(EntityTypeBuilder<Movimentacao> builder)
    {
        builder.HasKey(e => e.Movimentacao_Id);
        builder.Property(e => e.Data).IsRequired();
        builder.Property(e => e.Valor).IsRequired();
        builder.Property(e => e.Descricao).IsRequired(false);
        builder.Property(e => e.Entrada).IsRequired();

        builder.HasOne(x => x.Cartao)
            .WithMany(x => x.Movimentacoes)
            .HasForeignKey(x => x.Cartao_Id);

        builder.HasOne(x => x.Banco)
            .WithMany(x => x.Movimentacoes)
            .HasForeignKey(x => x.Banco_Id);

        builder.HasOne(x => x.NomeMovimentacao)
            .WithMany(x => x.Movimentacoes)
            .HasForeignKey(x => x.NomeMovimentacao_Id);
    }
}
