using Data.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UtilitySerivices
{
    public class GenericCrud<T> : IGenericCrud<T> where T : class
    {
        private readonly IGenericRepository<T> repo;

        public GenericCrud(IGenericRepository<T> repo)
        {
            this.repo = repo;
        }

        public async Task<T> AddEntity(T entity) => await repo.AddAsync(entity);

        public async Task<bool> DeleteEntity(int id) => await repo.DeleteAsync(id);

        public async Task<IEnumerable<T>> GetAllEntityes() => await repo.GetAllAsync();

        public async Task<T> GetByIdEntity(int id) => await repo.GetByIdAsync(id);

        public async Task<T> UpdateEntity(T entity) => await repo.UpdateAsync(entity);
    }
}
