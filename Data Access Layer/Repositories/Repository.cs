using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    class Repository<T> : IRepository<T> where T: class, IEntity
    {
        private DbContext _context;
        protected DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        virtual public async Task Create(T item)
        {
            await dbSet.AddAsync(item);
        }

        virtual public async Task Delete(int id)
        {
             dbSet.Remove(await dbSet.FindAsync(id));
        }

        virtual public async Task<T> Get(int id)
        {
             return await dbSet.FindAsync(id);
        }

        virtual public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        virtual public async Task Update(int id, T item)
        {
            var old = await dbSet.FindAsync(id);
            dbSet.Remove(old);
            await dbSet.AddAsync(item);
        }


    }
}
