using System;
using System.ComponentModel.DataAnnotations;

namespace ApiToBeSecured.Models
{
    public class Shipment
    {
        // that id would come from identity server in claims
        [Required]
        public string UserId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public Guid ProductId { get; set; }
    }
}