using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace apis
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly PublicacionesRepository _PublicacionesRepository;
        private readonly UserManager<IdentityUser> _usermanager;
        public ApiController(PublicacionesRepository PublicacionesRepository, UserManager<IdentityUser> usermanager)
        {
            _PublicacionesRepository = PublicacionesRepository;
            _usermanager = usermanager;

        }
        [HttpGet]
        public async Task<ActionResult<List<PublicacionesDTO>>> Userinfo( )
        {
            var user = await _usermanager.FindByNameAsync("chicadasilva");
            var list = await _PublicacionesRepository.GetAllpubbyuserApi(user);
            return list ;

        }

        // GET api/<ApiController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
