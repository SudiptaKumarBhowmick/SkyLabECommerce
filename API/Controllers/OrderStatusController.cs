using API.Response;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderStatusController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderStatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        public IActionResult UpdateOrderStatus(int id, [FromBody] OrderStatus orderStatus)
        {
            if (id.Equals(0))
            {
                return BadRequest("Invalid request!");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _unitOfWork.OrderStatusRepository.Update(id, orderStatus);
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
                return BadRequest("Order status not found");
            }

            _unitOfWork.OrderStatusRepository.Delete(orderStatus);
            var response = new SingleResponseModel<int>();
            response.Model = _unitOfWork.Save();
            return response.ToHttpDeletedResponse();
        }
    }
}
