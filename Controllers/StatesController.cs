using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreApiBooks.Repositories;
using CoreApiBooks.Models;

namespace CoreApiBooks.Controllers
{
    [Route("api/States")]
    public class StatesController : Controller
    {
        private readonly IStatesRepository repository;

        public StatesController(IStatesRepository repository)
        {
            this.repository = repository;
        }

        // GET api/people
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        // GET api/people/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await repository.FindAsync(id);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        // POST api/people
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]State model)
        {
            if (model == null)
                return BadRequest("Please provide a State.");

            var entity = await repository.AddAsync(model);

            return new CreatedAtRouteResult(entity, new { id = entity.ID });        
        }

        // PUT api/people/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]State model)
        {
            if (model == null || model.ID != id)
            {
                return BadRequest();
            }

            await repository.UpdateAsync(model);

            return new NoContentResult();
        }

        // DELETE api/people/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await repository.RemoveAsync(id);
            }
            catch (Exception ex)
            {                
                return BadRequest("No state found with id " + id);
            }

            return new NoContentResult();
        }
    }
}
