using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionLibrarySantiago
{
    public class VehicleModel
    {
        [Key]
        public int VehicleModelId { get; set; }
        public string VehicleModelName { get; set; }
        public int VehicleMakeId { get; set; }
        [ForeignKey("VehicleMakeId")] //Object Relational Mapper (Entity Framework)
        public VehicleMake VehicleMake { get; set; }
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public VehicleModel() { } //For EF

        public VehicleModel(string vehicleModelName, int vehicleMakeId)
        {
            VehicleModelName = vehicleModelName;
            VehicleMakeId = vehicleMakeId;
        }

    }
}
