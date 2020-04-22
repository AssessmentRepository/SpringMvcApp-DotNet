using SpringMvc.Datalayer;
using SpringMvc.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpringMvc.BusinessLayer.Repository
{
  public class AdminRepository: BaseRepository<Admin> ,IAdminRepository
    {
        public AdminRepository(IMongoUserDBContext context) : base(context)
        {
        }
    }
}
