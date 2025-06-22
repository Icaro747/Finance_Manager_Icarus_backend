namespace Finance_Manager_Icarus.Dtos
{
    /// <summary>
    /// DTO para atualizar um Banco existente.
    /// </summary>
    public class AtualizarBancoDto
    {
        public Guid Banco_Id { get; set; }
        public required string Nome { get; set; }
        public required string Numero { get; set; }
    }

    /// <summary>
    /// DTO para criar um novo Banco.
    /// </summary>
    public class CriarBancoDto
    {
        public required string Nome { get; set; }
        public required string Numero { get; set; }
    }

    /// <summary>
    /// DTO para listar Banco.
    /// </summary>
    public class ListarBancoDto
    {
        public Guid Banco_Id { get; set; }
        public required string Nome { get; set; }
        public required string Numero { get; set; }
    }
}
