using Finance_Manager_Icarus.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Finance_Manager_Icarus.Configuration;

public class TipoMovimentacaoConfiguration : IEntityTypeConfiguration<TipoMovimentacao>
{
    public void Configure(EntityTypeBuilder<TipoMovimentacao> builder)
    {
        builder.HasKey(e => e.Tipo_Movimentacao_Id);
        builder.Property(e => e.Nome).IsRequired();

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.TipoMovimentacoes)
            .HasForeignKey(x => x.Usuario_Id);
    }
}
