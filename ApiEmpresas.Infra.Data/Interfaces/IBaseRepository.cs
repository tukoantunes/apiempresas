using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Infra.Data.Interfaces
{
    /// <summary>
    /// Interface genérica para definir os métodos base do repositório
    /// </summary>
    /// <typeparam name="TEntity">Tipo genérico da entidade do repositório</typeparam>
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        #region Métodos abstratos

        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        List<TEntity> GetAll();
        TEntity GetById(Guid id);

        #endregion
    }
}



