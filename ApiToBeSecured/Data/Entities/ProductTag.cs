using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiToBeSecured.Models
{
    public class ProductTag
    {
        [Required]
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        
        [Required]
        public Guid TagId { get; set; }

        [JsonIgnore]
        public Tag Tag { get; set; }
    }
}