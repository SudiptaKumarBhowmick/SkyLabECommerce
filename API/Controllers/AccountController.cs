using API.Response;
using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var errorResponse = new ResponseModel();

            bool isUsername = false;
            string regEmail = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,10}$";
            if(!string.IsNullOrEmpty(loginDto.Email))
            {
                if(Regex.Match(loginDto.Email, regEmail).Success)
                {
                    var isEmailExists = await _unitOfWork.AccountRepository.IsEmailExists(loginDto.Email);
                    if (!isEmailExists)
                    {
                        errorResponse.Message = "Incorrect user or password!!";
                        return errorResponse.ToHttpErrorResponse();
                    }
                }
                else
                {
                    errorResponse.Message = "Invalid email address!!";
                    return errorResponse.ToHttpErrorResponse();
                }
            }
            else if(!string.IsNullOrEmpty(loginDto.UserName))
            {
                var isUsernameExists = await _unitOfWork.AccountRepository.IsUsernameExists(loginDto.UserName);
                if (isUsernameExists)
                {
                    isUsername = true;
                }
                else
                {
                    errorResponse.Message = "Incorrect user or password!!";
                    return errorResponse.ToHttpErrorResponse();
                }
            }
            else
            {
                errorResponse.Message = "Username or email is required!!";
                return errorResponse.ToHttpErrorResponse();
            }

            if(!string.IsNullOrEmpty(loginDto.Password))
            {
                var userEntity = await _unitOfWork.AccountRepository.IsUserExists(isUsername, loginDto);
                if(userEntity != null)
                {
                    var userDto = _mapper.Map<UserDto>(userEntity);
                    userDto.Token = _tokenService.CreateToken(_mapper.Map<UserTokenGenerationInformation>(userEntity));

                    var response = new SingleResponseModel<UserDto>();
                    response.Model = userDto;
                    return response.ToHttpResponse();
                }
                else
                {
                    errorResponse.Message = "Incorrect user or password!!";
                    return errorResponse.ToHttpErrorResponse();
                }
            }
            else
            {
                errorResponse.Message = "Password is required!!";
                return errorResponse.ToHttpErrorResponse();
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            var errorResponse = new ResponseModel();

            var isUserNameExists = await _unitOfWork.AccountRepository.IsUsernameExists(registerDto.UserName);
            if (isUserNameExists)
            {
                errorResponse.Message = "Username already exists";
                return errorResponse.ToHttpErrorResponse();
            }

            if (!string.IsNullOrEmpty(registerDto.UserEmail))
            {
                var isEmailExists = await _unitOfWork.AccountRepository.IsEmailExists(registerDto.UserEmail);
                if (isEmailExists)
                {
                    errorResponse.Message = "Email already exists";
                    return errorResponse.ToHttpErrorResponse();
                }
            }

            var userEntity = _mapper.Map<User>(registerDto);
            await _unitOfWork.AccountRepository.CreateUserAsync(userEntity);
            _unitOfWork.Save();

            var userDto = _mapper.Map<UserDto>(userEntity);
            userDto.Token = _tokenService.CreateToken(_mapper.Map<UserTokenGenerationInformation>(userEntity));

            var response = new SingleResponseModel<UserDto>();
            response.Model = userDto;
            return response.ToHttpResponse();
        }
    }
}
