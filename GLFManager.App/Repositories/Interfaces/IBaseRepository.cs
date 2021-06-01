using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity, TPrimaryKey>
    {
        Task<TEntity> Create(TEntity src);
        Task<TEntity> Get(TPrimaryKey id);
        Task<List<TEntity>> GetAll();
        Task Delete(Guid id);
    }
}
