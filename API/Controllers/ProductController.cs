using API.Response;
using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            var response = new SingleResponseModel<Product>();
            response.Model = product;

            return response.ToHttpResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();

            var response = new ListResponseModel<Product>();
            response.Model = products;

            return response.ToHttpListResponse();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);

            await _unitOfWork.ProductRepository.AddAsync(productEntity);

            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();

            return response.ToHttpCreatedResponse();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                var errorResponse = new ResponseModel();
                errorResponse.Message = "Product not found";
                return errorResponse.ToHttpErrorResponse();
            }

            var productEntity = _mapper.Map(productDto, product);

            _unitOfWork.ProductRepository.Update(productEntity);

            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();

            return response.ToHttpUpdatedResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                var errorResponse = new ResponseModel();
                errorResponse.Message = "Product not found";
                return errorResponse.ToHttpErrorResponse();
            }

            _unitOfWork.ProductRepository.Delete(product);
            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();
            return response.ToHttpDeletedResponse();
        }
    }
}
