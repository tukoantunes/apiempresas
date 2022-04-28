using ApiEmpresas.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiEmpresas.Services
{
    /// <summary>
    /// Classe para configuração da autenticação do projeto (JWT)
    /// </summary>
    public class JWTConfiguration
    {
        /// <summary>
        /// Método para registrar a configuração
        /// </summary>
        public static void Register(WebApplicationBuilder builder)
        {
            #region Lendo os parametros para geração do token contidos no appsettings.json

            var settings = builder.Configuration.GetSection("TokenSettings");
            builder.Services.Configure<TokenSettings>(settings);

            #endregion

            #region Configurando a aplicação para autenticação por JWT

            var section = settings.Get<TokenSettings>();
            var key = Encoding.ASCII.GetBytes(section.SecretKey);

            builder.Services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(auth =>
            {
                auth.RequireHttpsMetadata = false;
                auth.SaveToken = true;
                auth.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddTransient(map => new TokenCreator(section));

            #endregion
        }
    }
}



