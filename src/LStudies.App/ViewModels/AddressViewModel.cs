using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LStudies.App.ViewModels
{
    public class AddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(200, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 2)]
        [DisplayName("Public Place")]
        public string PublicPlace { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 2)]
        public string Number { get; set; }

        public string Complement { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(8, ErrorMessage = "{0} must have {1}", MinimumLength = 8)]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 2)]
        public string District { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 2)]
        [DisplayName("Country")]
        public string State { get; set; }

        [HiddenInput]
        public Guid ProviderId { get; set; }
    }
}
