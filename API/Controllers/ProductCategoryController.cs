using API.Response;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductCategoryController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductCategoryById(int id)
        {
            var productCategory = await _unitOfWork.ProductCategoryRepository.GetByIdAsync(id);

            var response = new SingleResponseModel<ProductCategory>();
            response.Model = productCategory;

            return response.ToHttpResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductCategories()
        {
            var productCategories = await _unitOfWork.ProductCategoryRepository.GetAllAsync();

            var response = new ListResponseModel<ProductCategory>();
            response.Model = productCategories;

            return response.ToHttpListResponse();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductCategory([FromBody] ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _unitOfWork.ProductCategoryRepository.AddAsync(productCategory);
            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();
            return response.ToHttpCreatedResponse();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProductCategory(int id, [FromBody] ProductCategory productCategory)
        {
            if (id.Equals(0))
            {
                return BadRequest("Invalid request!");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _unitOfWork.ProductCategoryRepository.Update(id, productCategory);
            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();
            return response.ToHttpUpdatedResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            var productCategory = await _unitOfWork.ProductCategoryRepository.GetByIdAsync(id);
            if (productCategory == null)
            {
                var errorResponse = new ResponseModel();
                errorResponse.Message = "Product category not found";
                return errorResponse.ToHttpErrorResponse();
            }

            _unitOfWork.ProductCategoryRepository.Delete(productCategory);
            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();
            return response.ToHttpDeletedResponse();
        }
    }
}
