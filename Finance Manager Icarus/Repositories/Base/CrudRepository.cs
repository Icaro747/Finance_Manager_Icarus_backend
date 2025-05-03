using Finance_Manager_Icarus.Data;
using Microsoft.EntityFrameworkCore;

namespace Finance_Manager_Icarus.Repositories.Base;

/// <summary>
/// Classe responsável por realizar operações CRUD (Create, Read, Update, Delete) em entidades do tipo T.
/// </summary>
/// <typeparam name="T">O tipo da entidade para a qual as operações CRUD serão executadas.</typeparam>
public class CrudRepository<T> : MainRepository where T : class
{
    protected DbSet<T> _dbSet;

    /// <summary>
    /// Cria uma nova instância do CrudRepository com o contexto do banco de dados fornecido.
    /// </summary>
    /// <param name="context">O contexto do banco de dados.</param>
    protected CrudRepository(FinanceManagerIcarusContext context) : base(context)
    {
        _dbSet = context.Set<T>();
    }

    /// <summary>
    /// Obtém uma entidade pelo seu ID.
    /// </summary>
    /// <param name="id">O ID da entidade a ser recuperada.</param>
    /// <returns>A entidade encontrada ou null caso não exista.</returns>
    public virtual T? GetById(Guid id)
    {
        return _dbSet.Find(id);
    }

    /// <summary>
    /// Obtém todas as entidades do tipo T.
    /// </summary>
    /// <returns>Uma lista de todas as entidades do tipo T no banco de dados.</returns>
    public virtual IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    /// <summary>
    /// Adiciona uma nova entidade do tipo T ao banco de dados.
    /// </summary>
    /// <param name="entity">A entidade que será adicionada.</param>
    public virtual void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    /// <summary>
    /// Atualiza uma entidade existente no banco de dados.
    /// </summary>
    /// <param name="entity">A entidade com as alterações a serem aplicadas.</param>
    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    /// <summary>
    /// Remove uma entidade do banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser removida.</param>
    public virtual void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    /// <summary>
    /// Salva todas as alterações feitas no contexto do banco de dados de forma síncrona.
    /// </summary>
    /// <returns>True se as alterações foram salvas com sucesso, caso contrário, false.</returns>
    public virtual bool SaveAll()
    {
        if (_context.SaveChanges() <= 0)
            throw new Exception("Nenhuma alteração foi salva no banco de dados.");

        return true;
    }


    /// <summary>
    /// Salva todas as alterações feitas no contexto do banco de dados de forma assíncrona.
    /// </summary>
    /// <returns>True se as alterações foram salvas com sucesso, caso contrário, false.</returns>
    public virtual async Task<bool> SaveAllAsync()
    {
        if (await _context.SaveChangesAsync() <= 0)
            throw new Exception("Nenhuma alteração foi salva no banco de dados.");

        return true;
    }

    /// <summary>
    /// Executa uma operação dentro de uma transação no banco de dados de forma assíncrona.
    /// Caso ocorra um erro, a transação será revertida.
    /// </summary>
    /// <param name="operation">A operação a ser executada dentro da transação.</param>
    public async Task ExecuteTransactionAsync(Func<Task> operation)
    {
        // Inicia uma transação assíncrona
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Executa a operação fornecida
            await operation();
            // Se a operação for bem-sucedida, confirma a transação
            await transaction.CommitAsync();
        }
        catch
        {
            // Em caso de erro, reverte a transação
            await transaction.RollbackAsync();
            throw; // Relança a exceção para ser tratada posteriormente
        }
    }
}
