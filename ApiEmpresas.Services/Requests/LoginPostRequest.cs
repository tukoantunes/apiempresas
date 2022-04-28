using System.ComponentModel.DataAnnotations;

namespace ApiEmpresas.Services.Requests
{
    /// <summary>
    /// Modelo de dados para a requisição de autenticação
    /// </summary>
    public class LoginPostRequest
    {
        [Required(ErrorMessage = "Por favor, informe o login.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Por favor, informe a senha.")]
        public string Senha { get; set; }
    }
}



