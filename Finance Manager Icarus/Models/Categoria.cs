using Finance_Manager_Icarus.Models.Base;

namespace Finance_Manager_Icarus.Models;

public class Categoria : EntidadeBase
{
    public Guid Categoria_Id { get; set; }
    public required string Nome { get; set; }

    public Guid Usuario_Id { get; set; }
    public virtual required Usuario Usuario { get; set; }

    public ICollection<NomeMovimentacao>? NomeMovimentacoes { get; set; }
}
