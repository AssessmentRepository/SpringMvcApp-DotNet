using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpringMvc.Datalayer
{
  public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> SignIn(string username, string Password);
        void Create(TEntity obj);
        void Update(TEntity obj);
        void Delete(string id);
        Task<TEntity> Get(string id);
        Task<IEnumerable<TEntity>> Get();
    }
}
