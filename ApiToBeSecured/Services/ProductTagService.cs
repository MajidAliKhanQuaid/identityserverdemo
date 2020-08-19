using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ApiToBeSecured.Data;
using ApiToBeSecured.Models;
using ApiToBeSecured.ViewModels;

namespace  ApiToBeSecured.Services
{
    public class ProductTagService : IProductTagService
    {
        private readonly ECommDbContext _context;
        public ProductTagService(ECommDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductTag>> GetAllProductTag()
        {
            return await _context.ProductTags.ToArrayAsync();
        }
    }
}