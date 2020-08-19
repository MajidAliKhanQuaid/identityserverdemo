using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTBS_MediatR.Categories.Queries;
using ApiTBS_MediatR.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiToBeSecured.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Api Working");
        }

        [HttpGet("list")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize]
        public async Task<IEnumerable<Category>> List()
        {
            return await _mediator.Send(new GetAllCategoriesQuery() { });
        }
    }
}
