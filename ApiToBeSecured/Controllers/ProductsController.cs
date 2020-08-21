using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiToBeSecured.ViewModels;
using ApiToBeSecured.Services;
using ApiToBeSecured.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Collections;
using System.Collections.Generic;

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
            // this method is written to avoid api remembing the host of the api
            IncludeHostUrlToProductImages(isSuccessResult);
            //
            return Json(isSuccessResult);
        }

        [HttpGet("{id}", Name = "ProductGet")]
        public async Task<IActionResult> Get(Guid id)
        {
            var isSuccessResult = await _productService.GetProductById(id);
            if (isSuccessResult == null) return BadRequest();
            IncludeHostUrlToProductImages(isSuccessResult);
            return Json(isSuccessResult);
        }

        // [Authorize(Policy = nameof(Constants.AdministratorRole))]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductDto model)
        {
            bool containsGuid = Guid.TryParse(model.Id, out var guid) && guid != Guid.Empty;
            // empty guid means new product
            if (!containsGuid)
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
            else
            {
                string result = await _productService.EditProductById(guid, model);
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

        private void IncludeHostUrlToProductImages(Product product)
        {
            Uri baseUri = new Uri(Request.Scheme + "://" + Request.Host.Value);
            //
            if (product.Image != null && product.Image.Count > 0)
            {
                foreach (var image in product.Image)
                {
                    Uri imageUri = new Uri(baseUri, image.Path);
                    //
                    image.Path = imageUri.ToString();
                }
            }
        }

        private void IncludeHostUrlToProductImages(IEnumerable<Product> products)
        {
            Uri baseUri = new Uri(Request.Scheme + "://" + Request.Host.Value);
            //
            foreach (var product in products)
            {
                if (product.Image != null && product.Image.Count > 0)
                {
                    foreach (var image in product.Image)
                    {
                        Uri imageUri = new Uri(baseUri, image.Path);
                        //
                        image.Path = imageUri.ToString();
                    }
                }
            }
        }

    }
}