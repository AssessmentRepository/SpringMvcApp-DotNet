using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpringMvc.Datalayer
{
  public  interface IMongoUserDBContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name);
    }
}
