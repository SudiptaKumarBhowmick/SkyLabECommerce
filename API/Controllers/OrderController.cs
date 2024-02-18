using API.Response;
using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);

            var response = new SingleResponseModel<Order>();
            response.Model = order;

            return response.ToHttpResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();

            var response = new ListResponseModel<Order>();
            response.Model = orders;

            return response.ToHttpListResponse();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var orderEntity = _mapper.Map<Order>(orderDto);

            await _unitOfWork.OrderRepository.AddAsync(orderEntity);

            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();

            return response.ToHttpCreatedResponse();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto orderDto)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                var errorResponse = new ResponseModel();
                errorResponse.Message = "Order not found";
                return errorResponse.ToHttpErrorResponse();
            }

            var orderEntity = _mapper.Map(orderDto, order);

            _unitOfWork.OrderRepository.Update(orderEntity);

            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();

            return response.ToHttpUpdatedResponse();
        }
    }
}
