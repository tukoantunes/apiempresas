using ApiEmpresas.Infra.Data.Interfaces;
using ApiEmpresas.Infra.Data.Repositories;

namespace ApiEmpresas.Services
{
    /// <summary>
    /// Classe para configuração da injeção de dependência do projeto
    /// </summary>
    public class DependencyInjection
    {
        /// <summary>
        /// Método para registrar e configurar as dependências
        /// </summary>
        public static void Register(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("ApiEmpresas");

            builder.Services.AddTransient<IEmpresaRepository>(map => new EmpresaRepository(connectionString));
            builder.Services.AddTransient<IFuncionarioRepository>(map => new FuncionarioRepository(connectionString));
            builder.Services.AddTransient<IUsuarioRepository>(map => new UsuarioRepository(connectionString));
        }
    }
}



