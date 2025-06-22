namespace Finance_Manager_Icarus.Dtos
{
    public class GraficoDto
    {
        public required string Type { get; set; } // Ex: "line", "column", "pie", etc.
        public List<string>? Labels { get; set; } // Usado para gráficos que têm eixo X textual
        public bool? DynamicScale { get; set; }   // Usado apenas se for relevante para o tipo
        public required List<DatasetDto> Datasets { get; set; }
    }

    public class DatasetDto
    {
        public required string Name { get; set; }         // Nome da série (ex: "Despesas")
        public List<decimal>? Data { get; set; }          // Valores (para line, column, pie, etc.)
        public string? Type { get; set; }                 // Usado em "column_and_line" para diferenciar
    }

    public class ScatterDatasetDto
    {
        public required string Name { get; set; }
        public required List<List<decimal>> Data { get; set; } // Ex: [[x, y], [x, y], ...]
    }

    /// <summary>
    /// DTO para retorno das análises financeiras comparativas
    /// </summary>
    public class AnaliseFinanceiraDto
    {
        /// <summary>
        /// Título da análise (ex: "Comparado ao Ano Atual")
        /// </summary>
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Valor atual da análise
        /// </summary>
        public decimal ValorAtual { get; set; }

        /// <summary>
        /// Variação percentual em relação à média histórica
        /// </summary>
        public decimal VariacaoPercentual { get; set; }

        /// <summary>
        /// Status da análise: "normal", "atencao" ou "alerta"
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Valor da média de comparação (opcional)
        /// </summary>
        public decimal? MediaComparacao { get; set; }

        /// <summary>
        /// Mensagem explicativa da análise
        /// </summary>
        public string Mensagem { get; set; } = string.Empty;

        /// <summary>
        /// Cor sugerida para exibição baseada no status
        /// </summary>
        public string Cor => Status switch
        {
            "normal" => "#22c55e",   // Verde
            "atencao" => "#f59e0b",  // Laranja
            "alerta" => "#ef4444",   // Vermelho
            _ => "#6b7280"           // Cinza padrão
        };

        /// <summary>
        /// Ícone sugerido para exibição baseado no status
        /// </summary>
        public string Icone => Status switch
        {
            "normal" => "pi pi-check",
            "atencao" => "pi pi-exclamation-triangle",
            "alerta" => "pi pi-exclamation-circle",
            _ => "pi pi-ban"
        };
    }
}
