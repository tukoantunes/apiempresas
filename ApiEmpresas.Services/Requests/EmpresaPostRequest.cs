using System.ComponentModel.DataAnnotations;

namespace ApiEmpresas.Services.Requests
{
    /// <summary>
    /// Modelo de dados da requisição de cadastro de empresa da API
    /// </summary>
    public class EmpresaPostRequest
    {
        [Required(ErrorMessage = "Informe o nome fantasia.")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "Informe a razão social.")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Informe o cnpj.")]
        public string Cnpj { get; set; }

        //lista de funcionários
        public List<EmpresaFuncionarioPostRequest>? Funcionarios { get; set; }
    }
}



