using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
  private readonly DataContext _dbContext;
        public ValuesController(DataContext dbContext)
        {
           _dbContext=dbContext;
        }
 [AllowAnonymous]
        // GET api/values
        [HttpGet]
        public async Task<IActionResult>  GetValues()
        {
            var values=await _dbContext.Values.ToListAsync();

            return Ok(values);
        }

        // GET api/values/5
       
        [HttpGet("{id}")]
        public  async Task<IActionResult> GetValues(int Id)
        {
            var values=await _dbContext.Values.FirstOrDefaultAsync(m=>m.id==Id);
            return Ok(values);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
