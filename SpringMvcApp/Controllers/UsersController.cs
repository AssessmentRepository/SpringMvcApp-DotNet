using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpringMvc.BusinessLayer.Repository;
using SpringMvc.Entities;

namespace SpringMvcApp.Controllers
{

    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository productRepository)
        {
            _userRepository = productRepository;
        }

        [HttpGet]
        [Route("api/user")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var user = await _userRepository.Get();
            return Ok(user);
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
            try
            {
                if (string.IsNullOrWhiteSpace(model.UserName))
                    return BadRequest("Please enter Name");
                else if (string.IsNullOrWhiteSpace(model.Password))
                    return BadRequest("Please enter Password");
                else if (string.IsNullOrWhiteSpace(model.Email))
                    return BadRequest("Please enter Email");

                _userRepository.Create(model);

                return Ok("Your product has been added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpPut("{id}")]
        public void Put( [FromBody] User model)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
