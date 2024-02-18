using API.Response;
using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderStatusController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderStatusController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderStatusById(int id)
        {
            var orderStatus = await _unitOfWork.OrderStatusRepository.GetByIdAsync(id);

            var response = new SingleResponseModel<OrderStatus>();
            response.Model = orderStatus;

            return response.ToHttpResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderStatuses()
        {
            var orderStatuses = await _unitOfWork.OrderStatusRepository.GetAllAsync();

            var response = new ListResponseModel<OrderStatus>();
            response.Model = orderStatuses;

            return response.ToHttpListResponse();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderStatus([FromBody] OrderStatus orderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _unitOfWork.OrderStatusRepository.AddAsync(orderStatus);
            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();
            return response.ToHttpCreatedResponse();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatusDto orderStatusDto)
        {
            var orderStatus = await _unitOfWork.OrderStatusRepository.GetByIdAsync(id);
            if (orderStatus == null)
            {
                var errorResponse = new ResponseModel();
                errorResponse.Message = "Order status not found";
                return errorResponse.ToHttpErrorResponse();
            }

            var orderStatusEntity = _mapper.Map(orderStatusDto, orderStatus);

            _unitOfWork.OrderStatusRepository.Update(orderStatusEntity);

            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();
            return response.ToHttpUpdatedResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatus(int id)
        {
            var orderStatus = await _unitOfWork.OrderStatusRepository.GetByIdAsync(id);
            if (orderStatus == null)
            {
                var errorResponse = new ResponseModel();
                errorResponse.Message = "Order status not found";
                return errorResponse.ToHttpErrorResponse();
            }

            _unitOfWork.OrderStatusRepository.Delete(orderStatus);
            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();
            return response.ToHttpDeletedResponse();
        }
    }
}
