using System.ComponentModel.DataAnnotations;

namespace ApiEmpresas.Services.Requests
{
    /// <summary>
    /// Modelo de dados da requisição de edição de empresa da API
    /// </summary>
    public class EmpresaPutRequest
    {
        [Required(ErrorMessage = "Informe o id da empresa.")]
        public Guid IdEmpresa { get; set; }

        [Required(ErrorMessage = "Informe o nome fantasia.")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "Informe a razão social.")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Informe o cnpj.")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "Informe o status ativo 1 ou 0.")]
        public int Ativo { get; set; }
    }
}



