using Finance_Manager_Icarus.Models.Base;

namespace Finance_Manager_Icarus.Models;

public class Mapeamento : EntidadeBase
{
    public Guid Mapeamento_Id { get; set; }
    public required string Nome { get; set; }

    public Guid Usuario_Id { get; set; }
    public virtual Usuario? Usuario { get; set; }

    public ICollection<ItensMapeamento>? ItensMapeamentos { get; set; }
}
