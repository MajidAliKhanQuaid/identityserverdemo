using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTBS_MediatR.Models
{
    public static class HardCode
    {
        public static List<Product> Products { get; set; } = new List<Product> {
                new Product() { Id = 1, Name = "Tube Light", Price = 30},
                new Product() { Id = 2, Name = "Refrigerator", Price = 30},
                new Product() { Id = 3, Name = "Laptop", Price = 30},
            };
        public static List<Category> Categories { get; set; } = new List<Category> {
                new Category() { Id = 1, Name = "Electronics",},
                new Category() { Id = 2, Name = "Appliances"},
                new Category() { Id = 3, Name = "Movie Watchers"},
            };
    }
}
