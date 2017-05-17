using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApiBooks.Repositories;
using CoreApiBooks.Models;

namespace CoreApiBooks.Controllers
{    
    [Route("api/Genres")]
    public class GenresController : Controller
    {
        private IRepository<Genre> repository;

        public GenresController(IRepository<Genre> repository)
        {
            this.repository = repository;
        }

        // GET: api/genres
        [HttpGet]
        public IActionResult Get()
        {            
            return Ok(repository.GetAll());
        }

        // POST api/genres
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Genre model)
        {           
            if (model == null)
                return BadRequest("Please provide a Genre.");

            var entity = await repository.AddAsync(model);

            return new CreatedAtRouteResult(entity, new { id = entity.ID });
            
        }

        // GET api/genres/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await repository.FindAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        // PUT api/genres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Genre model)
        {
            if (model == null || model.ID != id)
            {
                return BadRequest();
            }

            await repository.UpdateAsync(model);
           
            return new NoContentResult();
        }

        // DELETE api/genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = await repository.FindAsync(id);
                await repository.RemoveAsync(entity);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                return BadRequest($"No Genre found with id {id}");
            }

            return new NoContentResult();
        }
    }
}
