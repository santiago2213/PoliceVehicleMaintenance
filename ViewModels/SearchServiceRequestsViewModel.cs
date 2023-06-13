using DiscussionLibrarySantiago;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;

namespace DiscussionMvcSantiago.ViewModels
{
    public class SearchServiceRequestsViewModel
    {
        //public Vehicle? SearchVehicle { get; set; }
        public string? SearchVIN { get; set; }
        public string? SearchMechanicFullName { get; set; }  
        //public int? SearchVehicleId { get; set; }

        //[DataType(DataType.Date)]
        //public DateTime? SearchDateServiceRequested { get; set; }

        //public string? SearchOfficer { get; set; }
        //public string? SearchDescription { get; set; }



        public List<ServiceRequest> Result { get; set; }
    }
}
