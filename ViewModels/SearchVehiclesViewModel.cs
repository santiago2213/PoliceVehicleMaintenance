using DiscussionLibrarySantiago;
using System.ComponentModel.DataAnnotations;

namespace DiscussionMvcSantiago.ViewModels
{
    public class SearchVehiclesViewModel
    {
        //3 inputs
        public int? SearchMileage { get; set; }

        [DataType(DataType.Date)]           //Creates calendar in UI
        public DateTime? SearchPurchaseDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SearchServicedDate { get; set; }

        public VehicleStatusOptions? VehicleStatus { get; set; }

        public int? SearchVehicleMakeId { get; set; }    

        public int? SearchVehicleModelId { get; set; } 
        
        public string? ModelYear { get; set; }

        //Result
        public List<Vehicle> SearchResult { get; set; }

    }
}
