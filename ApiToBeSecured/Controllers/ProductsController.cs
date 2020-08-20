using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiToBeSecured.ViewModels;
using ApiToBeSecured.Services;
using ApiToBeSecured.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace ApiToBeSecured.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var isSuccessResult = await _productService.GetAllProduct();
            if (isSuccessResult == null) return BadRequest();
            return Json(isSuccessResult);
        }

        [HttpGet("{id}", Name = "ProductGet")]
        public async Task<IActionResult> Get(Guid id)
        {
            var isSuccessResult = await _productService.GetProductById(id);
            if (isSuccessResult == null) return BadRequest();
            return Json(isSuccessResult);
        }

        // [Authorize(Policy = nameof(Constants.AdministratorRole))]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductDto model)
        {
            var isSuccessResult = await _productService.AddProduct(model);

            //check if returned result is guid or not
            //if guid it was successfull. Otherwise unsuccessfull
            Guid GuidOutput;
            bool isGuid = Guid.TryParse(isSuccessResult, out GuidOutput);

            if (!isGuid)
                return BadRequest(isSuccessResult);
            else
            {
                //var NewUri = Url.Link("ProductGet",new{id = new Guid(isSuccessResult)});
                //return Created(NewUri,model);
                return Ok();
            }
        }


        [HttpPost("update", Name = "ProductUpdate")]
        public async Task<IActionResult> Update(Guid id, [FromForm] ProductDto model)
        {
            var isSuccessResult = await _productService.GetProductById(id);
            if (isSuccessResult == null || model == null) return BadRequest();
            var result = _productService.EditProductById(id, model);
            return Ok(new { result });
        }

        [HttpPut("{id}/{stock}")]
        public async Task<IActionResult> Put(Guid id, int stock)
        {
            var isSuccessResult = await _productService.EditProductStockById(id, stock);

            if (isSuccessResult == "Unsucessfull")
                return BadRequest();
            else
            {
                return Ok();
            }
        }

        // [Authorize(Policy = nameof(Constants.AdministratorRole))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isSuccessResult = await _productService.DeleteProductById(id);

            if (!isSuccessResult)
                return BadRequest();
            else
            {
                return Ok();
            }
        }
    }
}