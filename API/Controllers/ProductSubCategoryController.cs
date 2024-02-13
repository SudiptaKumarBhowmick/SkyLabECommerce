using API.Response;
using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductSubCategoryController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductSubCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductSubCategoryById(int id)
        {
            var productSubCategory = await _unitOfWork.ProductSubCategoryRepository.GetByIdAsync(id);

            var response = new SingleResponseModel<ProductSubCategory>();
            response.Model = productSubCategory;

            return response.ToHttpResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductSubCategories()
        {
            var productSubCategories = await _unitOfWork.ProductSubCategoryRepository.GetAllAsync();

            var response = new ListResponseModel<ProductSubCategory>();
            response.Model = productSubCategories;

            return response.ToHttpListResponse();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductSubCategory([FromBody] ProductSubCategoryDto productSubCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var productSubCategoryEntity = _mapper.Map<ProductSubCategory>(productSubCategoryDto);

            await _unitOfWork.ProductSubCategoryRepository.AddAsync(productSubCategoryEntity);

            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();

            return response.ToHttpCreatedResponse();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProductSubCategory(int id, [FromBody] ProductSubCategoryDto productSubCategoryDto)
        {
            if (id.Equals(0))
            {
                return BadRequest("Invalid request!");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var productSubCategoryEntity = _mapper.Map<ProductSubCategory>(productSubCategoryDto);

            _unitOfWork.ProductSubCategoryRepository.Update(id, productSubCategoryEntity);

            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();

            return response.ToHttpUpdatedResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductSubCategory(int id)
        {
            var productSubCategory = await _unitOfWork.ProductSubCategoryRepository.GetByIdAsync(id);
            if (productSubCategory == null)
            {
                var errorResponse = new ResponseModel();
                errorResponse.Message = "Product subcategory not found";
                return errorResponse.ToHttpErrorResponse();
            }

            _unitOfWork.ProductSubCategoryRepository.Delete(productSubCategory);
            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();
            return response.ToHttpDeletedResponse();
        }
    }
}
