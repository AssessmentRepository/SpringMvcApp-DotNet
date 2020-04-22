using SpringMvc.Datalayer;
using SpringMvc.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpringMvc.BusinessLayer.Repository
{
   public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoUserDBContext context) : base(context)
        {
        }
    }
}
