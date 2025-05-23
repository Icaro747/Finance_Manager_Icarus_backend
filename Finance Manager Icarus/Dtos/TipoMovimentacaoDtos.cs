﻿namespace Finance_Manager_Icarus.Dtos
{
    /// <summary>
    /// DTO para atualizar um TipoMovimentacao existente.
    /// </summary>
    public class AtualizarTipoMovimentacaoDto
    {
        public Guid Tipo_movimentacao_id { get; set; }
        public required string Nome { get; set; }
    }

    /// <summary>
    /// DTO para criar um novo TipoMovimentacao.
    /// </summary>
    public class CriarTipoMovimentacaoDto
    {
        public required string Nome { get; set; }
    }

    /// <summary>
    /// DTO para listar TipoMovimentacao.
    /// </summary>
    public class ListarTipoMovimentacaoDto
    {
        public Guid tipo_movimentacao_id { get; set; }
        public required string Nome { get; set; }
    }
}
