using GLFManager.App.Exceptions;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories
{
    public class BaseRepository<TEntity, TPrimaryKey, TDbContext> : IBaseRepository<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TDbContext : DbContext
    {

        protected readonly TDbContext _context;
        protected DbSet<TEntity> _entityDbSet;

        public BaseRepository(TDbContext context)
        {
            _context = context;
            _entityDbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> Create(TEntity src)
        {
            _entityDbSet.Add(src);
            await _context.SaveChangesAsync();

            return src;
        }

        public async Task<TEntity> Get(TPrimaryKey id)
        {
            var result = await _entityDbSet.FindAsync(id).ConfigureAwait(false);

            if (result == null)
                throw new NotFoundException("Item with Id: " + id + " not found");

            return result;
        }

        public async Task<List<TEntity>> GetAll()
        {
            var result = await _entityDbSet.ToListAsync();
            return result;
        }

        public async Task Delete(Guid id)
        {
            var result = await _entityDbSet.FindAsync(id).ConfigureAwait(false);

            if (result == null)
                throw new NotFoundException("TEntity " + id + "not found");

            _entityDbSet.Remove(result);
            await _context.SaveChangesAsync();
        }


    }
}
