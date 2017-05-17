using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CoreApiBooks.Data;
using CoreApiBooks.Models;

namespace CoreApiBooks.Repositories
{
    public interface ICitiesRepository
    {
        Task<City> AddAsync(City item);
        IEnumerable<City> GetAll();
        Task<City> FindAsync(int CityId);
        Task RemoveAsync(int CityId);
        Task UpdateAsync(City City);
    }

    public class CitiesRepository : ICitiesRepository
    {
        private readonly BooksDbContext context;

        public CitiesRepository(BooksDbContext context)
        {
            this.context = context;
        }

        public async Task<City> AddAsync(City item)
        {
            this.context.Entry(item.State).State = EntityState.Unchanged;

            context.Add(item);

            await context.SaveChangesAsync();

            return item;
        }

        public IEnumerable<City> GetAll()
        {
            return context.Cities.Include(c => c.State).Include(c => c.State.Country);
        }

        public async Task<City> FindAsync(int id)
        {
            return await context.Cities.Include(c => c.State).FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await context.Cities.SingleOrDefaultAsync(c => c.ID == id);

            if (entity == null)
                throw new InvalidOperationException($"No city found matching {id} ");
            
            context.Cities.Remove(entity);

            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(City item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.context.Entry(item.State).State = EntityState.Unchanged;

            context.Cities.Update(item);

            await context.SaveChangesAsync();
        }
    }
}
