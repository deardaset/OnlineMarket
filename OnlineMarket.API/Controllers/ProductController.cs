using Amazon.S3.Model.Internal.MarshallTransformations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineMarket.Application.Interfaces.Product;
using OnlineMarket.Application.Services.ProductServices;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;

namespace OnlineMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromServices] ICreateProductService service, [FromForm] CreateProductRequest request)
        {
            var result = await service.RunAsync(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductAsync([FromServices] IGetAllProductsService service, [FromQuery] GetAllProductsParametersRequest request)
        {
            var result = await service.RunAsync(request);
            return Ok(result);
        }

        [HttpPut]
        [Route("{guid}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid guid, [FromServices] IUpdateProductService service, [FromForm] UpdateProductRequest request)
        {
            var result = await service.RunAsync(guid, request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{guid}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid guid, [FromServices] IDeleteProductService service)
        {
            await service.RunAsync(guid);
            return Ok();
        }
    }
}
