namespace Finance_Manager_Icarus.Dtos
{
    /// <summary>
    /// DTO para atualizar um Categoria existente.
    /// </summary>
    public class AtualizarCategoriaDto
    {
        public Guid Categoria_id { get; set; }
        public required string Nome { get; set; }
    }

    /// <summary>
    /// DTO para criar um novo Categoria.
    /// </summary>
    public class CriarCategoriaDto
    {
        public required string Nome { get; set; }
    }

    /// <summary>
    /// DTO para listar Categoria.
    /// </summary>
    public class ListarCategoriaDto
    {
        public Guid Categoria_id { get; set; }
        public required string Nome { get; set; }
    }
}
