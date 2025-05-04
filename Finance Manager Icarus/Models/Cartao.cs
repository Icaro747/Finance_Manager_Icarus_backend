using Finance_Manager_Icarus.Models.Base;

namespace Finance_Manager_Icarus.Models;

public class Cartao : EntidadeBase
{
    public Guid Cartao_Id { get; set; }
    public required string Nome { get; set; }
    public required string Numero { get; set; }

    public Guid Banco_Id { get; set; }
    public virtual required Banco Banco { get; set; }

    public ICollection<Movimentacao>? Movimentacoes { get; set; }
}
