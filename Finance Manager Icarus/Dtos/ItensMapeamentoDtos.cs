namespace Finance_Manager_Icarus.Dtos
{
    /// <summary>
    /// DTO para atualizar um ItensMapeamento existente.
    /// </summary>
    public class AtualizarItensMapeamentoDto
    {
        public Guid Itens_Mapeamento_Id { get; set; }
        public required string Campo_Destino { get; set; }
        public required string Campo_Origem { get; set; }
    }

    /// <summary>
    /// DTO para criar um novo ItensMapeamento.
    /// </summary>
    public class CriarItensMapeamentoDto
    {
        public required string Campo_Destino { get; set; }
        public required string Campo_Origem { get; set; }
    }

    /// <summary>
    /// DTO para listar ItensMapeamento.
    /// </summary>
    public class ListarItensMapeamentoDto
    {
        public Guid Itens_Mapeamento_Id { get; set; }
        public required string Campo_Destino { get; set; }
        public required string Campo_Origem { get; set; }
    }
}
