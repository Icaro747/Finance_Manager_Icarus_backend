using Finance_Manager_Icarus.DTOs;
using Finance_Manager_Icarus.Repositories;
using Finance_Manager_Icarus.Services;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Manager_Icarus.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UsuarioRepository _usuariosRepository;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        ITokenService tokenService,
        UsuarioRepository usuariosRepository,
        ILogger<AuthController> logger
    )
    {
        _tokenService = tokenService;
        _usuariosRepository = usuariosRepository;
        _logger = logger;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var user = _usuariosRepository.ValidateUserCredentials(loginDto.Username, loginDto.Password);
            if (user != null)
            {
                _logger.LogInformation("User {Username} authenticated successfully", user.Nome);
                var token = _tokenService.CreateToken(user);

                LoginResponseDto obj = new()
                {
                    token = token,
                    nome = user.Nome
                };

                return Ok(obj);
            }

            _logger.LogWarning("Invalid login attempt for user {Username}", loginDto.Username);
            return Unauthorized("Credenciais inválidas.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during login for user {Username}", loginDto.Username);
            return StatusCode(500, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
        }
    }
}
