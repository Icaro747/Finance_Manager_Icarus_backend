using Finance_Manager_Icarus.Models.Base;

namespace Finance_Manager_Icarus.Models;

public class Banco : EntidadeBase
{
    public Guid Banco_Id { get; set; }
    public required string Nome { get; set; }
    public required string Numero { get; set; }

    public Guid Usuario_Id { get; set; }
    public virtual required Usuario Usuario { get; set; }

    public ICollection<Cartao>? Cartoes { get; set; }
    public ICollection<Movimentacao>? Movimentacoes { get; set; }
}
