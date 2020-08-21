using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ApiToBeSecured.Models;
using ApiToBeSecured.Data;
using ApiToBeSecured.ViewModels;
using System.IO;
using System.Linq;

namespace ApiToBeSecured.Services
{
    public class ProductService : IProductService
    {
        private readonly ECommDbContext _context;
        public ProductService(ECommDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _context.Products.Include(pro => pro.Tags).Include(pro => pro.Image).ToArrayAsync();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _context.Products.Include(pro => pro.Tags).Include(pro => pro.Image).Where(m => m.Id == id).SingleAsync();
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            _context.Products.Remove(await _context.Products.FindAsync(id));
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<string> AddProduct(ProductDto product)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var message = "";

            var entity = new Product
            {
                Id = new Guid(),
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };

            _context.Products.Add(entity);

            foreach (var image in product.Image.Files)
            {
                var extention = Path.GetExtension(image.FileName);
                if (allowedExtensions.Contains(extention.ToLower()) || image.Length > 2000000)
                    message = "Select jpg or jpeg or png less than 2Μ";
                var fileName = Path.Combine("Products", DateTime.Now.Ticks + extention);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }
                catch
                {
                    return "can not upload image";
                }

                var imageEntity = new Image
                {
                    Id = new Guid(),
                    ProductId = entity.Id,
                    Path = fileName
                };

                _context.Images.Add(imageEntity);
            }

            foreach (var tag in product.Tags)
            {
                var tagEntity = new ProductTag
                {
                    ProductId = entity.Id,
                    TagId = tag
                };
                _context.ProductTags.Add(tagEntity);
            }

            bool success = await _context.SaveChangesAsync() == 1 + product.Image.Files.Count + product.Tags.Count;

            if (success) return entity.Id.ToString();
            else return message;

        }

        public async Task<bool> DeleteProductById(Guid id)
        {
            _context.Products.Remove(await _context.Products.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<string> EditProductStockById(Guid id, int stock)
        {
            var entity = await _context.Products.FindAsync(id);

            if (entity.Stock - stock >= 0)
                entity.Stock = entity.Stock - stock;
            else return "Unsucessfull";

            bool success = await _context.SaveChangesAsync() == 1;
            if (success) return entity.Id.ToString();
            else return "Unsucessfull";
        }


        public async Task<string> EditProductById(Guid id, ProductDto productDto)
        {
            var entity = await this.GetProductById(id);
            entity.ProductName = productDto.ProductName;
            entity.Description = productDto.Description;
            entity.ProductCode = productDto.ProductCode;
            entity.Stock = productDto.Stock;
            //entity.Image = productDto.Image;
            //entity.Tags = productDto.Tags;
            //if (entity.Stock - stock >= 0)
            //    entity.Stock = entity.Stock - stock;
            //else return "Unsucessfull";
            await _context.SaveChangesAsync();
            return entity.Id.ToString();
        }


    }
}