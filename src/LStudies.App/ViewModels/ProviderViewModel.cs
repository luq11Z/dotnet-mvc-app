using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LStudies.App.ViewModels
{
    public class ProviderViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(200, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(14, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 11)]
        public string Document { get; set; }

        [DisplayName("Type")]
        public int ProviderType { get; set; }

        public AddressViewModel Address { get; set; }

        [DisplayName("Active?")]
        public bool IsActive { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
