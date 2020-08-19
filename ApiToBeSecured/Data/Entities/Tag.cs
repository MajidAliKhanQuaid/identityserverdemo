using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ApiToBeSecured.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        [Required]
        public string TagName { get; set; }

        [Required]
        public string TagDescription { get; set; }

        public List<ProductTag> Products { get; set; }
    }
}