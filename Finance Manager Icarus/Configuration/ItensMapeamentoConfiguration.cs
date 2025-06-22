using Finance_Manager_Icarus.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance_Manager_Icarus.Configuration;

public class ItensMapeamentoConfiguration : IEntityTypeConfiguration<ItensMapeamento>
{
    public void Configure(EntityTypeBuilder<ItensMapeamento> builder)
    {
        builder.HasKey(e => e.Itens_Mapeamento_Id);
        builder.Property(e => e.Campo_Destino).IsRequired();
        builder.Property(e=>e.Campo_Origem).IsRequired();

        builder.HasOne(x => x.Mapeamentos)
            .WithMany(x => x.ItensMapeamentos)
            .HasForeignKey(x => x.Mapeamento_Id);
    }
}
