namespace Finance_Manager_Icarus.Models.Base;

public class EntidadeBase
{
    public DateTime data_criacao { get; set; }
    public DateTime? data_atualizacao { get; set; }
    public DateTime? data_exclusao { get; set; }

    /// <summary>
    /// Atualiza a data de modificação da entidade para o momento atual.
    /// </summary>
    public void AddUpdateDate()
    {
        data_atualizacao = DateTime.UtcNow;
    }

    public void AddExclusaoDate()
    {
        data_exclusao = DateTime.UtcNow;
    }
}
