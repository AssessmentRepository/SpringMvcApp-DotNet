using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpringMvc.Datalayer
{
  public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> SignIn(string username, string Password);
        Task Create(TEntity obj);
        Task Update(TEntity obj);
        Task Delete(string id);
        Task<TEntity> Get(string id);
        Task<IEnumerable<TEntity>> Get();
    }
}
