using ApiEmpresas.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Infra.Data.Interfaces
{
    /// <summary>
    /// Interface para definição dos métodos do repositório de empresa
    /// </summary>
    public interface IEmpresaRepository : IBaseRepository<Empresa>
    {
        void Create(Empresa empresa, List<Funcionario> funcionarios);

        Empresa GetByRazaoSocial(string razaoSocial);
        Empresa GetByCnpj(string cnpj);
    }
}



