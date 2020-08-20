using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiToBeSecured.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        [JsonIgnore]

        public Product Product { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string Path { get; set; }
    }
}