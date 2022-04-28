namespace ApiEmpresas.Services
{
    /// <summary>
    /// Classe para configuração da política de CORS do projeto
    /// </summary>
    public class CorsConfiguration
    {
        //atributo
        private static string _CORS_POLICY = "DefaultPolicy";

        /// <summary>
        /// Método para registrar a configuração
        /// </summary>
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddCors(s => s.AddPolicy(_CORS_POLICY,
                    builder => {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    }));
        }

        /// <summary>
        /// Método para registrar a configuração
        /// </summary>
        public static void Use(WebApplication app)
        {
            app.UseCors(_CORS_POLICY);
        }
    }
}



