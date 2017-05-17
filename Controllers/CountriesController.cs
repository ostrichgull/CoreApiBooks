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
    [Route("api/Countries")]
    public class CountriesController : Controller
    {
        private IRepository<Country> repository;

        public CountriesController(IRepository<Country> repository)
        {
            this.repository = repository;
        }

        // GET: api/Countries
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        // POST api/Countries
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Country model)
        {
            if (model == null)
                return BadRequest("Please provide a Country.");

            var entity = await repository.AddAsync(model);

            return new CreatedAtRouteResult(entity, new { id = entity.ID });

        }

        // GET api/Countries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await repository.FindAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        // PUT api/Countries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Country model)
        {
            if (model == null || model.ID != id)
            {
                return BadRequest();
            }

            await repository.UpdateAsync(model);

            return new NoContentResult();
        }

        // DELETE api/Countries/5
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
                return BadRequest($"No Country found with id {id}");
            }

            return new NoContentResult();
        }
    }
}
