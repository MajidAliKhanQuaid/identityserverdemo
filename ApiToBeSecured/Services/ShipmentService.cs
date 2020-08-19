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
    public class ShipmentService : IShipmentService
    {
        private readonly ECommDbContext _context;
        public ShipmentService(ECommDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddShipment(ProductShipments shipment)
        {
            shipment.Id = Guid.NewGuid();
            foreach(ShipmentProductQuantity pro in shipment.productQuantity)
            {
                pro.Id = Guid.NewGuid();
                pro.ProductShipmentsId = shipment.Id;
                _context.ShipmentProductQuantity.Add(pro);
            }
            _context.ProductShipments.Add(shipment);
            bool success = await _context.SaveChangesAsync() == 1+shipment.productQuantity.Count;
            if(success) return shipment.Id.ToString();
            else return "Unsucessfull" ;
        }

        public async Task<IEnumerable<ProductShipments>> GetAllShipment()
        {
            return await _context.ProductShipments.ToArrayAsync();
        }

        public async Task<IEnumerable<ShipmentProductQuantity>> GetShipmentProductQuantityById(Guid id)
        {
            return await _context.ShipmentProductQuantity.Where( m=> m.ProductShipmentsId == id).ToArrayAsync();
        }
    }
}