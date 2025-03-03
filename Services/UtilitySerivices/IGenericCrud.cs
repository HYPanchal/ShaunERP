using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UtilitySerivices
{
    public interface IGenericCrud<T> where T : class
    {
        Task<IEnumerable<T>> GetAllEntityes();
        Task<T> GetByIdEntity(int id);
        Task<T> AddEntity(T entity);
        Task<T> UpdateEntity(T entity);
        Task<bool> DeleteEntity(int id);
    }
}
