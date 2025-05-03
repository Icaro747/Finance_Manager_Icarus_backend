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

    public Guid NomeMovimentacao_Id { get; set; }
    public virtual required NomeMovimentacao NomeMovimentacao { get; set; }
}
