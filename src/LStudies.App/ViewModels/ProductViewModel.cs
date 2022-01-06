using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LStudies.App.ViewModels
{
    public class ProductViewModel
    {
        [Key] //Difference the key from normal field.
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(200, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(200, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 2)]
        public string Description { get; set; }

        public IFormFile ImageUpload { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public decimal Price { get; set; }

        [ScaffoldColumn(false)] //disable inputing
        public DateTime CreatedAt { get; set; }

        [DisplayName("Active?")]
        public bool IsActive { get; set; }

        public ProviderViewModel Provider { get; set; }
    }
}
