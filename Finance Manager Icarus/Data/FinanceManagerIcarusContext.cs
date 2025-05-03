using Finance_Manager_Icarus.Configuration;
using Finance_Manager_Icarus.Models;
using Finance_Manager_Icarus.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Finance_Manager_Icarus.Data;

public class FinanceManagerIcarusContext : DbContext
{
    public FinanceManagerIcarusContext(DbContextOptions<FinanceManagerIcarusContext> options) : base(options)
    { }

    public DbSet<Usuario> usuario { get; set; }
    public DbSet<Banco> banco { get; set; }
    public DbSet<Cartao> cartao { get; set; }
    public DbSet<Categoria> categoria { get; set; }
    public DbSet<Movimentacao> movimentacao { get; set; }
    public DbSet<NomeMovimentacao> nome_movimentacao { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        modelBuilder.ApplyConfiguration(new BancoConfiguration());
        modelBuilder.ApplyConfiguration(new CartaoConfiguration());
        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new MovimentacaoConfiguration());
        modelBuilder.ApplyConfiguration(new NomeMovimentacaoConfiguration());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(EntidadeBase).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.Name)
                    .Property<DateTime>("data_criacao")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                modelBuilder.Entity(entityType.Name)
                    .Property<DateTime?>("data_exclusao")
                    .IsRequired(false);

                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime?>("data_atualizacao")
                    .IsRequired(false);
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Property("data_atualizacao").CurrentValue = DateTime.Now;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Property("data_exclusao").CurrentValue = DateTime.Now;
            }
        }

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
}
