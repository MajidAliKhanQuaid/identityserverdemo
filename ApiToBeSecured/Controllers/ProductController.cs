using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTBS_MediatR.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiToBeSecured.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("find/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product =  await _mediator.Send(new GetProductByIdQuery() { Id =  1});
            return Ok(product);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetProducts()
        {
            var products =  await _mediator.Send(new GetAllProductsQuery() { });
            return Ok(products);
        }
    }
}
