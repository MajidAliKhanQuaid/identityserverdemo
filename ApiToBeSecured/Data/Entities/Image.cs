using System;
using System.ComponentModel.DataAnnotations;

namespace ApiToBeSecured.Models
{
    public class Image
    {
        public Guid Id { get; set; }

        public Product Product { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string Path { get; set; }
    }
}