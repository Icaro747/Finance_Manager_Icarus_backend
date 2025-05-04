namespace Finance_Manager_Icarus.Dtos
{
    /// <summary>
    /// DTO para atualizar um Movimentacao existente.
    /// </summary>
    public class AtualizarMovimentacaoDto
    {
        public Guid Movimentacao_id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string? Descricao { get; set; }
        public bool Entrada { get; set; }
    }

    /// <summary>
    /// DTO para criar um novo Movimentacao.
    /// </summary>
    public class CriarMovimentacaoDto
    {
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string? Descricao { get; set; }
        public bool Entrada { get; set; }
        public Guid? Cartao_Id { get; set; }
        public Guid? Banco_Id { get; set; }
        public required string NomeMovimentacao { get; set; }
        public required string TipoMovimentacao { get; set; }
    }

    /// <summary>
    /// DTO para listar Movimentacao.
    /// </summary>
    public class ListarMovimentacaoDto
    {
        public Guid Movimentacao_id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string? Descricao { get; set; }
        public bool Entrada { get; set; }
    }
}
