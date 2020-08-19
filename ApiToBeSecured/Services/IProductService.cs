using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiToBeSecured.Models;
using ApiToBeSecured.ViewModels;

namespace ApiToBeSecured.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetProductById(Guid id);
        Task<string> AddProduct(ProductDto tag);
        Task<string> EditProductById(Guid id, int stock);
        Task<bool> DeleteProductById(Guid id);
    }
}