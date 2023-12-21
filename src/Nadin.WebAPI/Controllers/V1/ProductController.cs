using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nadin.Application.Features.ProductFeature.Commands.CreateProduct;
using Nadin.Application.Features.ProductFeature.Commands.DeleteProduct;
using Nadin.Application.Features.ProductFeature.Commands.UpdateProduct;
using Nadin.Application.Features.ProductFeature.Queries.GetProduct;
using Nadin.Application.Features.ProductFeature.Queries.GetProducts;
using SharedKernel.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.WebAPI.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductDto>>> Get([FromQuery] GetProductsQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductDto>> Get([FromRoute] GetProductQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpPut("{Id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] long Id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (Id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpGet("ABAC")]
        [Authorize]
        [Authorize(Policy = "GenderPolicy")]
        public IActionResult ABAC()
        {
            return Ok();
        }

        [HttpGet("CBAC")]
        [Authorize(Policy = "IPAccessControlPolicy")]
        public IActionResult CBAC()
        {
            return Ok();
        }
    }
}
