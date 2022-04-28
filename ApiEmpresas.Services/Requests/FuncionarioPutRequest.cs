using System.ComponentModel.DataAnnotations;

namespace ApiEmpresas.Services.Requests
{
    /// <summary>
    /// Modelo de dados da requisição de edição de funcionário da API
    /// </summary>
    public class FuncionarioPutRequest
    {
        [Required(ErrorMessage = "Por favor, informe o id do funcionário.")]
        public Guid IdFuncionario { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome do funcionário.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o cpf do funcionário.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Por favor, informe o matricula do funcionário.")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de admissão do funcionário.")]
        public DateTime DataAdmissao { get; set; }

        [Required(ErrorMessage = "Por favor, informe a empresa do funcionário.")]
        public Guid IdEmpresa { get; set; }

        [Required(ErrorMessage = "Informe o status ativo 1 ou 0.")]
        public int Ativo { get; set; }
    }
}




