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
    [Route("api/Genders")]
    public class GendersController : Controller
    {
        private IRepository<Gender> repository;

        public GendersController(IRepository<Gender> repository)
        {
            this.repository = repository;
        }

        // GET: api/Genders
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        // POST api/Genders
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Gender model)
        {
            if (model == null)
                return BadRequest("Please provide a Gender.");

            var entity = await repository.AddAsync(model);

            return new CreatedAtRouteResult(entity, new { id = entity.ID });

        }

        // GET api/Genders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await repository.FindAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        // PUT api/Genders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Gender model)
        {
            if (model == null || model.ID != id)
            {
                return BadRequest();
            }

            await repository.UpdateAsync(model);

            return new NoContentResult();
        }

        // DELETE api/Genders/5
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
                return BadRequest($"No Gender found with id {id}");
            }

            return new NoContentResult();
        }
    }
}
