using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiToBeSecured.Models;
using ApiToBeSecured.ViewModels;

namespace ApiToBeSecured.Services
{
    public interface IShipmentService
    {
        Task<IEnumerable<ProductShipments>> GetAllShipment();
        Task<string> AddShipment(ProductShipments shipment);

        Task<IEnumerable<ShipmentProductQuantity>> GetShipmentProductQuantityById(Guid id);
    }
}