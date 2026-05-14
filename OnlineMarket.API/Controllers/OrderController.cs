using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Application.Interfaces.Order;
using OnlineMarket.Application.Services.OrderServices;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using System.Security.Claims;

namespace OnlineMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromServices] ICreateOrderService service)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await service.RunAsync(userId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProductAsync([FromServices] IGetAllOrdersService service)
        {
            var result = await service.RunAsync();
            return Ok(result);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyOrdersAsync([FromServices] IGetMyOrdersService service)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await service.RunAsync(userId);
            return Ok(result);
        }

        [HttpGet]
        [Route("{guid}")]
        public async Task<IActionResult> GetOrderByIdAsync([FromServices] IGetOrderByIdService service, [FromRoute] Guid guid)
        {
            var result = await service.RunAsync(guid);
            return Ok(result);
        }

        [HttpPost("{orderId}/product/{productId}")]
        public async Task<IActionResult> AddProductToOrder([FromRoute] Guid orderId, [FromRoute] Guid productId, [FromServices] IAddProductToOrderService service)
        {
            var result = await service.RunAsync(orderId, productId);
            return Ok(result);
        }

        [HttpDelete("{orderId}/product/{productId}")]
        public async Task<IActionResult> RemoveProductFromOrder([FromRoute] Guid orderId, [FromRoute] Guid productId, [FromServices] IRemoveProductFromOrderService service)
        {
            var result = await service.RunAsync(orderId, productId);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{guid}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid guid, [FromServices] IDeleteOrderService service)
        {
            await service.RunAsync(guid);
            return Ok();
        }
    }
}
