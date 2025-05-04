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
}
