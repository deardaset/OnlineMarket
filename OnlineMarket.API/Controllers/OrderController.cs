using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Application.Services.OrderServices;
using OnlineMarket.Core.Exceptions;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Order;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using OnlineMarket.SharedKernel.Contracts.Enums;
using System.Security.Claims;

namespace OnlineMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromServices] ICreateOrderService service)
        {
            var result = await service.RunAsync(GetCurrentUserId());
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrdersAsync([FromServices] IGetAllOrdersService service, [FromQuery] GetAllOrdersParametersRequest request)
        {
            var result = await service.RunAsync(request);
            return Ok(result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyOrdersAsync([FromServices] IGetMyOrdersService service)
        {
            var result = await service.RunAsync(GetCurrentUserId());
            return Ok(result);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetOrderByIdAsync([FromServices] IGetOrderByIdService service, [FromRoute] Guid guid)
        {
            var result = await service.RunAsync(guid, GetCurrentUserId(), IsAdmin());
            return Ok(result);
        }

        [HttpPost("{orderId}/product/{productId}")]
        public async Task<IActionResult> AddProductToOrder([FromRoute] Guid orderId, [FromRoute] Guid productId, [FromServices] IAddProductToOrderService service)
        {
            var result = await service.RunAsync(orderId, productId, GetCurrentUserId(), IsAdmin());
            return Ok(result);
        }

        [HttpDelete("{orderId}/product/{productId}")]
        public async Task<IActionResult> RemoveProductFromOrder([FromRoute] Guid orderId, [FromRoute] Guid productId, [FromServices] IRemoveProductFromOrderService service)
        {
            var result = await service.RunAsync(orderId, productId, GetCurrentUserId(), IsAdmin());
            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] Guid guid, [FromServices] IDeleteOrderService service)
        {
            await service.RunAsync(guid, GetCurrentUserId(), IsAdmin());
            return Ok();
        }

        private Guid GetCurrentUserId()
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(rawUserId, out var userId))
                throw new OnlineMarketUnauthorizedException("Unauthorized");

            return userId;
        }

        private bool IsAdmin() => User.IsInRole(Roles.Admin.ToString());
    }
}
