using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpringMvc.Datalayer
{
  public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoUserDBContext _mongoContext;
        protected IMongoCollection<TEntity> _dbCollection;

        protected BaseRepository(IMongoUserDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }


        public async Task Create(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            var all = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty, null);
            return await all.ToListAsync();
        }

      

        public Task<TEntity> SignIn(string username, string Password)
        {
            throw new NotImplementedException();
        }

        public Task Update(TEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
