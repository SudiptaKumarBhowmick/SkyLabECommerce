using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Data;
using Data.DTOs;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Service.Extensions;

namespace Service.Helpers
{
    public class CloudinaryFileManager : ICloudinaryFileManager
    {
        public IConfiguration _configuration { get; }
        private CloudinarySettings _cloudinarySettings;
        private Cloudinary _cloudinary;

        public CloudinaryFileManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _cloudinarySettings = ApplicationServiceExtensions.GetCloudinarySettings(_configuration) ?? new CloudinarySettings();
            Account account = new Account(_cloudinarySettings.CloudName, _cloudinarySettings.ApiKey, _cloudinarySettings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ProductImage> AddProductImage(ProductImageDto productImageDto)
        {
            var file = productImageDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Folder = "/SkyLabECommerceProducts",
                        Transformation = new Transformation().Height(600).Width(400).Crop("fit")
                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
            }

            ProductImage dataToReturn = new ProductImage()
            {
                Id = 0,
                Url = uploadResult.Url.ToString(),
                PublicId = uploadResult.PublicId,
                IsMain = productImageDto.IsMain,
                ProductId = productImageDto.ProductId
            };

            return dataToReturn;
        }

        public bool DeleteProductImage(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);

            var result = _cloudinary.Destroy(deletionParams);

            if (result != null)
            {
                return true;
            }

            return false;
        }
    }
}
