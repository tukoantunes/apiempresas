using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiEmpresas.Services.Authentication
{
    /// <summary>
    /// Classe para fazer a geração do TOKEN
    /// </summary>
    public class TokenCreator
    {
        //atributo
        private readonly TokenSettings _tokenSettings;

        //construtor para injeção de dependência
        public TokenCreator(TokenSettings tokenSettings)
        {
            _tokenSettings = tokenSettings;
        }

        //método para fazer a geração do TOKEN
        public string GenerateToken(string login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.SecretKey);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, login) }),
                Expires = DateTime.Now.AddHours(_tokenSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}




