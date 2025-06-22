using Finance_Manager_Icarus.Models.Base;

namespace Finance_Manager_Icarus.Models;

public class ItensMapeamento : EntidadeBase
{
    public Guid Itens_Mapeamento_Id { get; set; }
    public required string Campo_Destino { get; set; }
    public required string Campo_Origem { get; set; }

    public Guid Mapeamento_Id { get; set; }
    public virtual Mapeamento? Mapeamentos { get; set; }
}
