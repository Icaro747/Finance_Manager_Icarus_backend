using Finance_Manager_Icarus.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Finance_Manager_Icarus.Configuration;

public class BancoConfiguration : IEntityTypeConfiguration<Banco>
{
    public void Configure(EntityTypeBuilder<Banco> builder)
    {
        builder.HasKey(e => e.Banco_Id);
        builder.Property(e => e.Nome).IsRequired();
        builder.Property(e => e.Numero).IsRequired();

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.Bancos)
            .HasForeignKey(x => x.Usuario_Id);
    }
}
