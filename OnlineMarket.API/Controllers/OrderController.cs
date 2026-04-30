using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Application.Services.Order;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;

namespace OnlineMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromServices] CreateOrderService service)
        {
            var result = await service.RunAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductAsync([FromServices] GetAllOrdersService service)
        {
            var result = await service.RunAsync();
            return Ok(result);
        }

        //[HttpPut]
        //[Route("{guid}")]
        //public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid guid, [FromServices] UpdateOrderService service, [FromForm] UpdateProductRequest request)
        //{
        //    var result = await service.RunAsync(guid, request);
        //    return Ok(result);
        //}

        [HttpDelete]
        [Route("{guid}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid guid, [FromServices] DeleteOrderService service)
        {
            await service.RunAsync(guid);
            return Ok();
        }
    }
}
