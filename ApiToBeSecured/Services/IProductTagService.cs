using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiToBeSecured.ViewModels;
using ApiToBeSecured.Models;

namespace ApiToBeSecured.Services
{
    public interface IProductTagService
    {
        Task<IEnumerable<ProductTag>> GetAllProductTag();
    }
}