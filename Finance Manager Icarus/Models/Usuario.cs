using Finance_Manager_Icarus.Models.Base;

namespace Finance_Manager_Icarus.Models;

public class Usuario : EntidadeBase
{
    public Guid Usuario_Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }

    public ICollection<Banco>? Bancos { get; set; }
    public ICollection<Categoria>? Categorias { get; set; }
    public ICollection<NomeMovimentacao>? NomeMovimentacoes { get; set; }
    public ICollection<TipoMovimentacao>? TipoMovimentacoes { get; set; }
    public ICollection<Mapeamento>? Mapeamentos { get; set; }

    public void HashPassword(string password)
    {
        Senha = BCrypt.Net.BCrypt.HashPassword(password);
    }
}