using Finance_Manager_Icarus.Models.Base;

namespace Finance_Manager_Icarus.Models;

/// <summary>
/// Representa a entidade TipoMovimentacao no sistema.
/// </summary>
public class TipoMovimentacao : EntidadeBase
{
    /// <summary>
    /// Identificador único da entidade TipoMovimentacao.
    /// </summary>
    public Guid Tipo_movimentacao_id { get; set; }

    /// <summary>
    /// Representa o campo Nome do tipo string.
    /// </summary>
    public required string Nome { get; set; }

    public Guid Usuario_Id { get; set; }
    public virtual Usuario? Usuario { get; set; }

    public ICollection<Movimentacao>? Movimentacoes { get; set; }
}
