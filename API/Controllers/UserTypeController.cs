using API.Response;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserTypeController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        public IActionResult UpdateUserType(int id, [FromBody] UserType userType)
        {
            if (id.Equals(0))
            {
                return BadRequest("Invalid request!");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _unitOfWork.UserTypeRepository.Update(id, userType);
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
