using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ApiToBeSecured.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public List<Image> Image { get; set; }

        public List<ProductTag> Tags { get; set; }
    }
}