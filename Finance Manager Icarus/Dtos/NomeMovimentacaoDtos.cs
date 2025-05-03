namespace Finance_Manager_Icarus.Dtos
{
    /// <summary>
    /// DTO para atualizar um NomeMovimentacao existente.
    /// </summary>
    public class AtualizarNomeMovimentacaoDto
    {
        public Guid Nome_movimentacao_id { get; set; }
        public required string Nome { get; set; }
    }

    /// <summary>
    /// DTO para criar um novo NomeMovimentacao.
    /// </summary>
    public class CriarNomeMovimentacaoDto
    {
        public required string Nome { get; set; }
    }

    /// <summary>
    /// DTO para listar NomeMovimentacao.
    /// </summary>
    public class ListarNomeMovimentacaoDto
    {
        public Guid Nome_movimentacao_id { get; set; }
        public required string Nome { get; set; }
    }
}
