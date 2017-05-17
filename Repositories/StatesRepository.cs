using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CoreApiBooks.Data;
using CoreApiBooks.Models;

namespace CoreApiBooks.Repositories
{
    public interface IStatesRepository
    {
        Task<State> AddAsync(State item);
        IEnumerable<State> GetAll();
        Task<State> FindAsync(int stateId);
        Task RemoveAsync(int stateId);
        Task UpdateAsync(State state);
    }

    public class StatesRepository : IStatesRepository
    {
        private readonly BooksDbContext context;

        public StatesRepository(BooksDbContext context)
        {
            this.context = context;
        }

        public async Task<State> AddAsync(State item)
        {
            this.context.Entry(item.Country).State = EntityState.Unchanged;

            context.Add(item);

            await context.SaveChangesAsync();

            return item;
        }

        public IEnumerable<State> GetAll()
        {
            return context.States.Include(s => s.Country);
        }

        public async Task<State> FindAsync(int id)
        {
            return await context.States.Include(s => s.Country).FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await context.States.SingleOrDefaultAsync(s => s.ID == id);

            if (entity == null)
                throw new InvalidOperationException($"No state found matching {id} ");

            context.States.Remove(entity);
                             
            await context.SaveChangesAsync();            
        }

        public async Task UpdateAsync(State item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.context.Entry(item.Country).State = EntityState.Unchanged;

            context.States.Update(item);

            await context.SaveChangesAsync();
        }
    }
}
