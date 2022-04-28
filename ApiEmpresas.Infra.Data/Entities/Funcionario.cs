using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Infra.Data.Entities
{
    /// <summary>
    /// Classe de modelo de dados
    /// </summary>
    public class Funcionario
    {
        #region Propriedades

        public Guid IdFuncionario { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Matricula { get; set; }
        public DateTime DataAdmissao { get; set; }
        public Guid IdEmpresa { get; set; }
        public int Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAlteracao { get; set; }

        #endregion

        #region Associações

        public Empresa Empresa { get; set; }

        #endregion
    }
}



