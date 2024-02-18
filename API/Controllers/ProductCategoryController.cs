using API.Response;
using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductCategoryController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        public async Task<IActionResult> UpdateProductCategory(int id, [FromBody] ProductCategoryDto productCategoryDto)
        {
            var productCategory = await _unitOfWork.ProductCategoryRepository.GetByIdAsync(id);
            if (productCategory == null)
            {
                var errorResponse = new ResponseModel();
                errorResponse.Message = "Product category not found";
                return errorResponse.ToHttpErrorResponse();
            }

            var productCategoryEntity = _mapper.Map(productCategoryDto, productCategory);
            _unitOfWork.ProductCategoryRepository.Update(productCategoryEntity);
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
