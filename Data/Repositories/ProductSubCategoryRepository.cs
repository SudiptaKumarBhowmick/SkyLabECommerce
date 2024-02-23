using Data.DTOs;
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
    public class ProductSubCategoryRepository : GenericRepository<ProductSubCategory>, IProductSubCategoryRepository
    {
        public ProductSubCategoryRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            
        }

        public async override Task<IEnumerable<ProductSubCategory>> GetAllAsync()
        {
            return await _applicationDBContext.ProductSubCategory.Include(entity => entity.ProductCategory).ToListAsync();
        }

        public async Task<IEnumerable<ProductSubCategory>> GetSubCategoryByCategory(int categoryId)
        {
            return await _applicationDBContext.ProductSubCategory.Where(subCategory => subCategory.ProductCategoryId.Equals(categoryId)).ToListAsync();
        }
    }
}
