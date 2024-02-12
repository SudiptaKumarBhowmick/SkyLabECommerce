using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDBContext _applicationDBContext;

        public GenericRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task AddAsync(T entity)
        {
            await _applicationDBContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _applicationDBContext.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _applicationDBContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _applicationDBContext.Set<T>().FindAsync(id);
        }

        public void Update(int id, T entity)
        {
            var data = _applicationDBContext.Set<T>().Find(id);

            if (data != null)
            {
                _applicationDBContext.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
