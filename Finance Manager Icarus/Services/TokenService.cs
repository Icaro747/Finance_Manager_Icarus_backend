using Finance_Manager_Icarus.Authentication;
using Finance_Manager_Icarus.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Finance_Manager_Icarus.Services
{
    /// <summary>
    /// Interface para serviço de geração de tokens JWT.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Gera um token JWT para um usuário autenticado.
        /// </summary>
        /// <param name="user">Objeto contendo os dados do usuário.</param>
        /// <returns>Uma string contendo o token JWT.</returns>
        string CreateToken(Usuario user);
    }

    /// <summary>
    /// Serviço responsável pela geração de tokens JWT.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly ILogger<TokenService> _logger;

        /// <summary>
        /// Construtor do TokenService.
        /// </summary>
        /// <param name="jwtConfig">Configuração do JWT.</param>
        public TokenService(IOptions<JwtConfig> jwtConfig, ILogger<TokenService> logger)
        {
            _jwtConfig = jwtConfig.Value;
            _logger = logger;
        }

        /// <summary>
        /// Cria um token JWT para um usuário autenticado.
        /// </summary>
        /// <param name="user">Objeto do usuário contendo os dados necessários.</param>
        /// <returns>Uma string representando o token JWT gerado.</returns>
        public string CreateToken(Usuario user)
        {
            _logger.LogInformation("Generating token for user: {Username}", user.Nome);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Nome), // Identificação do usuário
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // ID único do token
                new Claim(JwtRegisteredClaimNames.Sid, user.Usuario_Id.ToString()), // ID do usuário
                new Claim(ClaimTypes.Name, user.Nome) // Nome do usuário
            };

            var token = new JwtSecurityToken(
                _jwtConfig.ValidIssuer, // Emissor do token
                _jwtConfig.ValidAudience, // Destinatário do token
                claims,
                expires: DateTime.Now.AddMinutes(_jwtConfig.ExpiryMinutes), // Tempo de expiração do token
                signingCredentials: credentials // Credenciais de assinatura
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            _logger.LogInformation("Token generated: {Token}", tokenString);

            // Log the structure of the token for debugging purposes
            var parts = tokenString.Split('.');
            if (parts.Length == 3)
            {
                _logger.LogInformation("Token Header: {Header}", parts[0]);
                _logger.LogInformation("Token Payload: {Payload}", parts[1]);
                _logger.LogInformation("Token Signature: {Signature}", parts[2]);
            }
            else
            {
                _logger.LogError("Generated token is malformed: {Token}", tokenString);
            }

            return tokenString;
        }
    }
}
