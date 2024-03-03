using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            
        }

        public override async Task<IEnumerable<ProductImage>> GetAllAsync()
        {
            return await _applicationDBContext.ProductImage.Include(entity => entity.Product).ToListAsync();
        }
    }
}
