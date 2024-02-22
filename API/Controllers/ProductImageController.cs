using API.Response;
using Data.DTOs;
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
    }
}
