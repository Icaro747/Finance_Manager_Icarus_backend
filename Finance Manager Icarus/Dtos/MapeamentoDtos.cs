namespace Finance_Manager_Icarus.Dtos
{
    /// <summary>
    /// DTO para atualizar um Mapeamento existente.
    /// </summary>
    public class AtualizarMapeamentoDto
    {
        public Guid Mapeamento_Id { get; set; }
        public required string Nome { get; set; }
    }

    /// <summary>
    /// DTO para criar um novo Mapeamento.
    /// </summary>
    public class CriarMapeamentoDto
    {
        public required string Nome { get; set; }
        public required List<CriarItensMapeamentoDto> ItensMapeamentos { get; set; }
    }

    /// <summary>
    /// DTO para listar Mapeamento.
    /// </summary>
    public class ListarMapeamentoDto
    {
        public Guid Mapeamento_Id { get; set; }
        public required string Nome { get; set; }
        public required List<ListarItensMapeamentoDto> ItensMapeamentos { get; set; }
    }
}
