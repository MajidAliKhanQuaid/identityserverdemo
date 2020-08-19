using System;
using System.ComponentModel.DataAnnotations;

namespace ApiToBeSecured.Models
{
    public class BuyingList
    {
        public Guid Id { get; set; }

        // that id would come from identity server via claims

        [Required]
        public string UserId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public string BuyDate { get; set; }

        [Required]
        public bool IsShipped { get; set; }
    }
}