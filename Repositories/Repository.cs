using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CoreApiBooks.Data;

namespace CoreApiBooks.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        IEnumerable<T> GetAll();
        Task<T> FindAsync(int id);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
    }

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BooksDbContext context;
        private DbSet<T> entities;

        public Repository(BooksDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public async Task<T> AddAsync(T entity)
        {
            entities.Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> FindAsync(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entities.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            if (entity == null)
                throw new InvalidOperationException("No matching object found.");

            entities.Remove(entity);

            await context.SaveChangesAsync();
        }
    }
}
