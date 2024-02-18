using API.Response;
using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserTypeController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserTypeById(int id)
        {
            var userType = await _unitOfWork.UserTypeRepository.GetByIdAsync(id);

            var response = new SingleResponseModel<UserType>();
            response.Model = userType;

            return response.ToHttpResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserTypes()
        {
            var userTypes = await _unitOfWork.UserTypeRepository.GetAllAsync();

            var response = new ListResponseModel<UserType>();
            response.Model = userTypes;

            return response.ToHttpListResponse();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserType([FromBody] UserType userType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _unitOfWork.UserTypeRepository.AddAsync(userType);
            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();
            return response.ToHttpCreatedResponse();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserType(int id, [FromBody] UserTypeDto userTypeDto)
        {
            var userType = await _unitOfWork.UserTypeRepository.GetByIdAsync(id);
            if (userType == null)
            {
                var errorResponse = new ResponseModel();
                errorResponse.Message = "User type not found";
                return errorResponse.ToHttpErrorResponse();
            }

            var userTypeEntity = _mapper.Map(userTypeDto, userType);

            _unitOfWork.UserTypeRepository.Update(userTypeEntity);

            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();

            return response.ToHttpUpdatedResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserType(int id)
        {
            var userType = await _unitOfWork.UserTypeRepository.GetByIdAsync(id);
            if (userType == null)
            {
                var errorResponse = new ResponseModel();
                errorResponse.Message = "User type not found";
                return errorResponse.ToHttpErrorResponse();
            }

            _unitOfWork.UserTypeRepository.Delete(userType);
            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();
            return response.ToHttpDeletedResponse();
        }
    }
}
