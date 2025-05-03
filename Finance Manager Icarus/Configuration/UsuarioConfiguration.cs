using Finance_Manager_Icarus.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Finance_Manager_Icarus.Configuration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(e => e.Usuario_Id);
        builder.Property(e => e.Nome).IsRequired();
        builder.Property(e => e.Email).IsRequired();
        builder.Property(e => e.Senha).IsRequired();
    }
}
