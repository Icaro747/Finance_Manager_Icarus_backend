using Finance_Manager_Icarus.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Finance_Manager_Icarus.Configuration;

public class NomeMovimentacaoConfiguration : IEntityTypeConfiguration<NomeMovimentacao>
{
    public void Configure(EntityTypeBuilder<NomeMovimentacao> builder)
    {
        builder.HasKey(e => e.Nome_movimentacao_id);
        builder.Property(e => e.Nome).IsRequired();

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.NomeMovimentacoes)
            .HasForeignKey(x => x.Usuario_Id);

        builder.HasOne(x => x.Categoria)
            .WithMany(x => x.NomeMovimentacoes)
            .HasForeignKey(x => x.Categoria_Id);
    }
}
