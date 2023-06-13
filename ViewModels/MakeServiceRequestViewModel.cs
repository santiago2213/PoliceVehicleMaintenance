using DiscussionLibrarySantiago;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DiscussionMvcSantiago.ViewModels
{
    public class MakeServiceRequestViewModel
    {
        [Required(ErrorMessage = "Description is required")]
        public string  Description { get; set; }
        [Required(ErrorMessage = "VehicleId is required")]
        public int  VehicleId { get; set; }
        //public string? Officer { get; set; }
        //public DateTime? DateServiceRequested { get; set; }


        //public List<ServiceRequest> Result { get; set; }
    }
}
