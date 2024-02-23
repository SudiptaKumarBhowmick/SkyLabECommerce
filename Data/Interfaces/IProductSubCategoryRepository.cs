using Data.DTOs;
using Data.Entities;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IProductSubCategoryRepository : IGenericRepository<ProductSubCategory>
    {
        Task<IEnumerable<ProductSubCategory>> GetSubCategoryByCategory(int categoryId);
    }
}
