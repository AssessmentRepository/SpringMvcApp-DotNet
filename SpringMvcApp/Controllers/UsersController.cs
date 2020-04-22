using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpringMvc.BusinessLayer.Repository;
using SpringMvc.Entities;

namespace SpringMvcApp.Controllers
{
// [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
       [Route("api/user")]
     // [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            //Do code here
            return new List<User>();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            //Do code here
            return new User();
        }

        [HttpPost]
        [Route("api/user/addValues")]
        public IActionResult Post(User model)
        {
           //Do code here
            return Ok("Your user has been added successfully");
        }

    }
}
