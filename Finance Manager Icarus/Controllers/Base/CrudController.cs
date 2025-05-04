using AutoMapper;
using Finance_Manager_Icarus.Repositories;
using Finance_Manager_Icarus.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Manager_Icarus.Controllers.Base;

/// <summary>
/// Classe base para controllers que implementam operações CRUD com suporte a DTOs usando AutoMapper.
/// </summary>
/// <typeparam name="TEntity">O tipo da entidade do banco de dados.</typeparam>
/// <typeparam name="TRepository">O tipo do repositório que implementa CrudRepository<TEntity>.</typeparam>
/// <typeparam name="TCreateDto">O tipo do DTO usado para criação.</typeparam>
/// <typeparam name="TUpdateDto">O tipo do DTO usado para atualização.</typeparam>
/// <typeparam name="TReadDto">O tipo do DTO usado para leitura/listagem.</typeparam>
[ApiController]
[Route("[controller]")]
public abstract class CrudController<TEntity, TRepository, TCreateDto, TUpdateDto, TReadDto> : ControllerFinanceManagerIcarusData
    where TEntity : class
    where TRepository : CrudRepository<TEntity>
    where TCreateDto : class
    where TUpdateDto : class
    where TReadDto : class
{
    protected readonly TRepository _repository;
    protected readonly IMapper _mapper;

    /// <summary>
    /// Cria uma nova instância do CrudController com o repositório e mapeador fornecidos.
    /// </summary>
    /// <param name="repository">O repositório a ser utilizado para operações CRUD.</param>
    /// <param name="mapper">O AutoMapper para conversão entre entidades e DTOs.</param>
    protected CrudController(
        TRepository repository,
        IMapper mapper,
        UsuarioRepository usuarioRepository
    ) : base(usuarioRepository)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtém todos os registros da entidade convertidos para DTO de leitura.
    /// </summary>
    /// <returns>Uma lista de DTOs representando as entidades.</returns>
    [HttpGet]
    public virtual ActionResult<IEnumerable<TReadDto>> GetAll()
    {
        try
        {
            var entities = _repository.GetAll();
            var dtos = _mapper.Map<IEnumerable<TReadDto>>(entities);
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    /// <summary>
    /// Obtém um registro específico pelo seu ID, convertido para DTO de leitura.
    /// </summary>
    /// <param name="id">O ID do registro a ser recuperado.</param>
    /// <returns>O DTO representando a entidade encontrada ou NotFound caso não exista.</returns>
    [HttpGet("{id}")]
    public virtual ActionResult<TReadDto> GetById(Guid id)
    {
        try
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                return NotFound();

            var dto = _mapper.Map<TReadDto>(entity);
            return Ok(dto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    /// <summary>
    /// Cria um novo registro a partir de um DTO de criação.
    /// </summary>
    /// <param name="createDto">O DTO contendo os dados para criação.</param>
    /// <returns>O resultado da operação incluindo o DTO representando a entidade criada.</returns>
    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] TCreateDto createDto)
    {
        try
        {
            var entity = _mapper.Map<TEntity>(createDto);

            // Permitir personalização antes de salvar (como geração de ID)
            BeforeCreate(entity, createDto);

            _repository.Add(entity);
            await _repository.SaveAllAsync();

            // Permitir personalização após salvar
            AfterCreate(entity, createDto);

            var readDto = _mapper.Map<TReadDto>(entity);
            return CreatedAtAction(nameof(GetById), new { id = GetEntityId(entity) }, readDto);
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    /// <summary>
    /// Atualiza um registro existente a partir de um DTO de atualização.
    /// </summary>
    /// <param name="id">O ID do registro a ser atualizado.</param>
    /// <param name="updateDto">O DTO contendo os dados para atualização.</param>
    /// <returns>O resultado da operação.</returns>
    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(Guid id, [FromBody] TUpdateDto updateDto)
    {
        try
        {
            // Verifica se a entidade existe no banco de dados
            var existingEntity = _repository.GetById(id);
            if (existingEntity == null)
                return NotFound($"Entidade com ID {id} não encontrada.");

            // Permitir personalização antes de mapear
            BeforeUpdate(existingEntity, updateDto, id);

            // Atualiza a entidade com os dados do DTO usando AutoMapper
            _mapper.Map(updateDto, existingEntity);

            // Garantir que o ID seja mantido
            SetEntityId(existingEntity, id);

            // Permitir personalização após mapear
            AfterUpdate(existingEntity, updateDto, id);

            _repository.Update(existingEntity);
            await _repository.SaveAllAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    /// <summary>
    /// Remove um registro.
    /// </summary>
    /// <param name="id">O ID do registro a ser removido.</param>
    /// <returns>O resultado da operação.</returns>
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                return NotFound();

            // Permitir personalização antes de deletar
            BeforeDelete(entity, id);

            _repository.Delete(entity);
            await _repository.SaveAllAsync();

            // Permitir personalização após deletar
            AfterDelete(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest("Erro interno: " + ex);
        }
    }

    /// <summary>
    /// Método abstrato para obter o ID da entidade.
    /// </summary>
    /// <param name="entity">A entidade.</param>
    /// <returns>O ID da entidade.</returns>
    protected abstract Guid GetEntityId(TEntity entity);

    /// <summary>
    /// Método abstrato para definir o ID da entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser modificada.</param>
    /// <param name="id">O ID a ser definido.</param>
    protected abstract void SetEntityId(TEntity entity, Guid id);

    /// <summary>
    /// Método que pode ser sobrescrito para personalizar a entidade antes de criar.
    /// </summary>
    /// <param name="entity">A entidade que será criada.</param>
    /// <param name="createDto">O DTO de criação.</param>
    protected virtual void BeforeCreate(TEntity entity, TCreateDto createDto) { }

    /// <summary>
    /// Método que pode ser sobrescrito para personalizar a entidade após criar.
    /// </summary>
    /// <param name="entity">A entidade que foi criada.</param>
    /// <param name="createDto">O DTO de criação.</param>
    protected virtual void AfterCreate(TEntity entity, TCreateDto createDto) { }

    /// <summary>
    /// Método que pode ser sobrescrito para personalizar a entidade antes de atualizar.
    /// </summary>
    /// <param name="entity">A entidade que será atualizada.</param>
    /// <param name="updateDto">O DTO de atualização.</param>
    /// <param name="id">O ID da entidade.</param>
    protected virtual void BeforeUpdate(TEntity entity, TUpdateDto updateDto, Guid id) { }

    /// <summary>
    /// Método que pode ser sobrescrito para personalizar a entidade após atualizar.
    /// </summary>
    /// <param name="entity">A entidade que foi atualizada.</param>
    /// <param name="updateDto">O DTO de atualização.</param>
    /// <param name="id">O ID da entidade.</param>
    protected virtual void AfterUpdate(TEntity entity, TUpdateDto updateDto, Guid id) { }

    /// <summary>
    /// Método que pode ser sobrescrito para personalizar o processo antes de deletar.
    /// </summary>
    /// <param name="entity">A entidade que será deletada.</param>
    /// <param name="id">O ID da entidade.</param>
    protected virtual void BeforeDelete(TEntity entity, Guid id) { }

    /// <summary>
    /// Método que pode ser sobrescrito para personalizar o processo após deletar.
    /// </summary>
    /// <param name="id">O ID da entidade que foi deletada.</param>
    protected virtual void AfterDelete(Guid id) { }
}