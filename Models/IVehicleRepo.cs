using DiscussionLibrarySantiago;

namespace DiscussionMvcSantiago.Models
{
    public interface IVehicleRepo   //What should be done, Not HOW (implementation)
    {
        public IEnumerable<Vehicle> GetAllVehicles();
        public List<VehicleModel> GetAllVehicleModels();
        public List<VehicleMake> GetAllVehicleMakes();
        public int AddVehicle(Vehicle vehicle);
        public Vehicle FindVehicle(int vehicleId);
        public void UpdateVehicle(Vehicle vehicle); 
    }

 
}
