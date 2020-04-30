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
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _productRepository;

        public AdminController(IAdminRepository Repository)
        {
            _productRepository = Repository;
        }

        [HttpGet]
        [Route("api/admin")]
        public async Task<ActionResult<IEnumerable<Admin>>> Get()
        {
            //Do code here
            return new List<Admin>();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> Get(string id)
        {
            //Do code here

            return new Admin();
        }

        [HttpPost]
        [Route("api/admin/addValues")]
        public IActionResult Post(User model)
        {
            //Do code here

            return Ok("Your product has been added successfully");
        }

        [HttpPut("{id}")]
        public void Put([FromBody] User model)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}