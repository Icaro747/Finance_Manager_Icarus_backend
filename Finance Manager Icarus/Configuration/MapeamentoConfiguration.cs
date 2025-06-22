using Finance_Manager_Icarus.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance_Manager_Icarus.Configuration;

public class MapeamentoConfiguration : IEntityTypeConfiguration<Mapeamento>
{
    public void Configure(EntityTypeBuilder<Mapeamento> builder)
    {
        builder.HasKey(e => e.Mapeamento_Id);
        builder.Property(e => e.Nome).IsRequired();

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.Mapeamentos)
            .HasForeignKey(x => x.Usuario_Id);
    }
}
