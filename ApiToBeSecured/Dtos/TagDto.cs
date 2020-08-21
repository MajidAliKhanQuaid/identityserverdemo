using System;
using System.ComponentModel.DataAnnotations;

namespace ApiToBeSecured.Services
{
    public class TagDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100,ErrorMessage="The {0} must be between {2} and {1} length",MinimumLength=2)]
        public string TagName { get; set; }

        [Required]
        [StringLength(1000,ErrorMessage="The {0} must be between {2} and {1} length",MinimumLength=2)]
        public string TagDescription { get; set; }
    }
}