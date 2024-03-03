using API.Response;
using Data.DTOs;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductImageController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryFileManager _cloudinaryFileManager;

        public ProductImageController(IUnitOfWork unitOfWork, ICloudinaryFileManager cloudinaryFileManager)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryFileManager = cloudinaryFileManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductImages()
        {
            var productImages = await _unitOfWork.ProductImageRepository.GetAllAsync();

            var response = new ListResponseModel<ProductImage>();
            response.Model = productImages;

            return response.ToHttpListResponse();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductImage([FromForm] ProductImageDto productImageDto)
        {
            var uploadResult = await _cloudinaryFileManager.AddProductImage(productImageDto);

            if (uploadResult is not null)
            {
                await _unitOfWork.ProductImageRepository.AddAsync(uploadResult);

                var response = new SingleResponseModel<int>();
                response.Model = _unitOfWork.Save();

                return response.ToHttpCreatedResponse();
            }

            var errorResponse = new ResponseModel();
            errorResponse.Message = "Failed to upload image";
            return errorResponse.ToHttpErrorResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var errorResponse = new ResponseModel();

            var productImage = await _unitOfWork.ProductImageRepository.GetByIdAsync(id);
            if (productImage == null)
            {
                errorResponse.Message = "Product image not found";
                return errorResponse.ToHttpErrorResponse();
            }

            var deletionResult = _cloudinaryFileManager.DeleteProductImage(productImage.PublicId);
            if (deletionResult)
            {
                _unitOfWork.ProductImageRepository.Delete(productImage);
                var response = new SingleResponseModel<int>();
                response.Model = _unitOfWork.Save();
                return response.ToHttpDeletedResponse();
            }

            errorResponse.Message = "Failed to delete product image";
            return errorResponse.ToHttpErrorResponse();
        }
    }
}
