using API.Response;
using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdminUserController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminUserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminUserById(int id)
        {
            var adminUser = await _unitOfWork.AdminUserRepository.GetByIdAsync(id);

            var response = new SingleResponseModel<AdminUser>();
            response.Model = adminUser;

            return response.ToHttpResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAdminUsers()
        {
            var adminUsers = await _unitOfWork.AdminUserRepository.GetAllAsync();

            var response = new ListResponseModel<AdminUser>();
            response.Model = adminUsers;

            return response.ToHttpListResponse();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdminUser([FromBody] AdminUserDto adminUserDto)
        {
            var adminUserEntity = _mapper.Map<AdminUser>(adminUserDto);

            await _unitOfWork.AdminUserRepository.AddAsync(adminUserEntity);

            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();

            return response.ToHttpCreatedResponse();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAdminUser(int id, [FromBody] AdminUserDto adminUserDto)
        {
            var adminUserEntity = _mapper.Map<AdminUser>(adminUserDto);

            _unitOfWork.AdminUserRepository.Update(id, adminUserEntity);

            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();

            return response.ToHttpUpdatedResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminUser(int id)
        {
            var adminUser = await _unitOfWork.AdminUserRepository.GetByIdAsync(id);
            if (adminUser == null)
            {
                var errorResponse = new ResponseModel();
                errorResponse.Message = "Admin user not found";
                return errorResponse.ToHttpErrorResponse();
            }

            _unitOfWork.AdminUserRepository.Delete(adminUser);
            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();
            return response.ToHttpDeletedResponse();
        }
    }
}
