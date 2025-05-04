using Finance_Manager_Icarus.Models.Base;

namespace Finance_Manager_Icarus.Models;

public class NomeMovimentacao : EntidadeBase
{
    public Guid Nome_movimentacao_id { get; set; }
    public required string Nome { get; set; }

    public Guid? Categoria_Id { get; set; }
    public virtual Categoria? Categoria { get; set; }

    public Guid Usuario_Id { get; set; }
    public virtual Usuario? Usuario { get; set; }

    public ICollection<Movimentacao>? Movimentacoes { get; set; }
}