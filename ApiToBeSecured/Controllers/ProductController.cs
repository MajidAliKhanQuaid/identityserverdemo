using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTBS_MediatR.Models;
using ApiTBS_MediatR.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiToBeSecured.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product =  await _mediator.Send(new GetProductByIdQuery() { Id =  1});
            return Ok(product);
        }

        [HttpGet]
        [Route("[controller]/all")]
        [Route("api/[controller]/all")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products =  await _mediator.Send(new GetAllProductsQuery() { });
            return products;
        }
    }
}
