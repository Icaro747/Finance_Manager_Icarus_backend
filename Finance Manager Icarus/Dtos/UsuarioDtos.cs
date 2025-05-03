namespace Finance_Manager_Icarus.Dtos
{
    /// <summary>
    /// DTO para atualizar um Usuario existente.
    /// </summary>
    public class AtualizarUsuarioDto
    {
        public Guid Usuario_Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }

    /// <summary>
    /// DTO para criar um novo Usuario.
    /// </summary>
    public class CriarUsuarioDto
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }

    /// <summary>
    /// DTO para listar Usuario.
    /// </summary>
    public class ListarUsuarioDto
    {
        public Guid Usuario_Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}
