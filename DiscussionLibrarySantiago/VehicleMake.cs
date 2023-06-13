using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DiscussionLibrarySantiago
{
    public class VehicleMake
    {
        [Key]
        public int VehicleMakeId { get; set; }
        public string VehicleMakeName { get; set; }
        public List<VehicleModel> VehicleModels { get; set; } = new List<VehicleModel>();
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle> { };

        public VehicleMake() { } //For EF
        public VehicleMake(string vehicleMakeName)
        {
            VehicleMakeName = vehicleMakeName;
        }
    }

    
}