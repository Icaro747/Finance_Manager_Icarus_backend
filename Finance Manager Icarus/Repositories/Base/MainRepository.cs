using Finance_Manager_Icarus.Data;

namespace Finance_Manager_Icarus.Repositories.Base;

/// <summary>
/// Classe abstrata que serve como base para a criação de repositórios no sistema.
/// Padroniza a injeção do contexto do banco de dados e permite o registro automático
/// dos repositórios no container de dependências, simplificando sua adição.
/// </summary>
public abstract class MainRepository
{
    protected readonly FinanceManagerIcarusContext _context;

    protected MainRepository(FinanceManagerIcarusContext context)
    {
        _context = context;
    }
}
