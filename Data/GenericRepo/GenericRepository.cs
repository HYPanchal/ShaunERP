using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.GenericRepo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CRMDBContext _context;

        private readonly DbSet<T> _dBSet;

        public GenericRepository(CRMDBContext context)
        {
            this._context = context;
            this._dBSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dBSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dBSet.FindAsync(id);
            if (entity == null) return false;
            _dBSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = await _context.Set<T>().AsNoTracking().ToListAsync();
            return query;
        }

        public async Task<T> GetByIdAsync(int id) => await _dBSet.FindAsync(id);

        public async Task<T> UpdateAsync(T entity)
        {
            _dBSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
