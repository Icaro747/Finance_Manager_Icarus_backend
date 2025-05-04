using Finance_Manager_Icarus.Models.Base;

namespace Finance_Manager_Icarus.Models;

public class Movimentacao : EntidadeBase
{
    public Guid Movimentacao_Id { get; set; }
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public string? Descricao { get; set; }
    public bool Entrada { get; set; }

    public Guid? Cartao_Id { get; set; }
    public virtual Cartao? Cartao { get; set; }

    public Guid? Banco_Id { get; set; }
    public virtual Banco? Banco { get; set; }

    public Guid Nome_movimentacao_Id { get; set; }
    public virtual required NomeMovimentacao NomeMovimentacao { get; set; }

    public Guid Tipo_movimentacao_Id { get; set; }
    public virtual required TipoMovimentacao TipoMovimentacao { get; set; }

    /// <summary>
    /// Verifica se a movimentação possui uma fonte válida de origem.
    /// Uma movimentação é considerada com fonte válida quando está associada a um cartão ou a um banco.
    /// Retorna true se pelo menos um dos dois estiver definido (com um Guid diferente de Guid.Empty).
    /// </summary>
    public bool PossuiFonteValida()
    {
        return (Cartao_Id.HasValue && Cartao_Id != Guid.Empty) ||
               (Banco_Id.HasValue && Banco_Id != Guid.Empty);
    }
}
