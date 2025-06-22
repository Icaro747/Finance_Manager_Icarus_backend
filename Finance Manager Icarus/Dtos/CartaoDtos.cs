namespace Finance_Manager_Icarus.Dtos
{
    /// <summary>
    /// DTO para atualizar um Cartao existente.
    /// </summary>
    public class AtualizarCartaoDto
    {
        public Guid Cartao_Id { get; set; }
        public required string Nome { get; set; }
        public required string Numero { get; set; }
    }

    /// <summary>
    /// DTO para criar um novo Cartao.
    /// </summary>
    public class CriarCartaoDto
    {
        public required string Nome { get; set; }
        public required string Numero { get; set; }
        public Guid Banco_Id { get; set; }
    }

    /// <summary>
    /// DTO para listar Cartao.
    /// </summary>
    public class ListarCartaoDto
    {
        public Guid Cartao_Id { get; set; }
        public required string Nome { get; set; }
        public required string Numero { get; set; }
    }
}
