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
    [Route("api/Cities")]
    public class CitiesController : Controller
    {
        private readonly ICitiesRepository repository;

        public CitiesController(ICitiesRepository repository)
        {
            this.repository = repository;
        }

        // GET api/cities
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        // GET api/cities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await repository.FindAsync(id);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        // POST api/cities
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]City model)
        {
            if (model == null)
                return BadRequest("Please provide a State.");

            var entity = await repository.AddAsync(model);

            return new CreatedAtRouteResult(entity, new { id = entity.ID });
        }

        // PUT api/cities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]City model)
        {
            if (model == null || model.ID != id)
            {
                return BadRequest();
            }

            await repository.UpdateAsync(model);

            return new NoContentResult();
        }

        // DELETE api/cities/5
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
