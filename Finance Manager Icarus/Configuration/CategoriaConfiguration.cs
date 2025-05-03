using Finance_Manager_Icarus.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Finance_Manager_Icarus.Configuration;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(e => e.Categoria_Id);
        builder.Property(e => e.Nome).IsRequired();

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.Categorias)
            .HasForeignKey(x => x.Usuario_Id);
    }
}
