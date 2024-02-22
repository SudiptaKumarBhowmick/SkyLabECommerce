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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _applicationDBContext.Product.Include(category => category.ProductCategory).Include(subCategory => subCategory.ProductSubCategory).ToListAsync();
        }
    }
}
