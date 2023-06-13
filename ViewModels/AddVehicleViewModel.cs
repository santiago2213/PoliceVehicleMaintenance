using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DiscussionMvcSantiago.ViewModels
{
    public class AddVehicleViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "VIN cannot be empty")]
        [StringLength(5, ErrorMessage ="VIN is 5 characters", MinimumLength =5)]
        public string? VIN { get; set; }
        [Required(ErrorMessage ="Purchase date is required")]
        [DataType(DataType.Date)]
        public DateTime? DatePurchased { get; set; }
        public int? Year { get; set; } 
        public int? Mileage { get; set; }
        [Required(ErrorMessage = "Vehicle make is required")]
        public int? VehicleMakeId { get; set; }
        [Required(ErrorMessage = "Vehicle model is required")]
        public int? VehicleModelId { get; set; }
    }
}
