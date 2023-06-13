using DiscussionLibrarySantiago;
using DiscussionMvcSantiago.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscussionMvcSantiago.Models
{
    public class VehicleRepo : IVehicleRepo
    {
        private ApplicationDbContext _database;

        //Inject database into a VehicleRepo object
        public VehicleRepo(ApplicationDbContext database)
        {
            _database= database;
        }

        public List<VehicleModel> GetAllVehicleModels()
        {
            List<VehicleModel> vehicleModels = _database.VehicleModel.ToList();
            return vehicleModels;   
        }

        public List<VehicleMake> GetAllVehicleMakes()
        {
            List<VehicleMake> vehicleMakes = _database.VehicleMake.ToList();
            return vehicleMakes;
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            //Query using Linq
            IEnumerable<Vehicle> allVehicles = _database.Vehicle
                .Include(v => v.VehicleServiceRequests)
                .Include(v => v.VehicleMake)
                .Include(v => v.VehicleModel)
                .Include(v => v.VehicleAddedBySupervisor);
                //.Where(v => v.VehicleServiceRequests.Any()) //only vehicles having SVR
                //.Where(v => v.VehicleServiceRequests.Count > 0)
                //.ToList();
            return allVehicles;



        }

        public int AddVehicle(Vehicle vehicle)
        {
            int vehicleId = 0;
            _database.Vehicle.Add(vehicle);
            try
            {
                _database.SaveChanges();
                vehicleId = vehicle.VehicleId;
            }
            catch (DbUpdateException dbUpdateException) 
            { 
                string errorMessage = dbUpdateException.Message;
                vehicleId = -1;
            }

            return vehicleId;
        }

        public Vehicle FindVehicle(int vehicleId) //Find: 1.PK    2.Only that class /table 
        {
            Vehicle vehicle = _database.Vehicle
                .Include(v => v.VehicleAddedBySupervisor)
                .Where(v => v.VehicleId == vehicleId).First();
            return vehicle;
        }

        public void UpdateVehicle(Vehicle modifiedVehicle)
        {
            _database.Vehicle.Update(modifiedVehicle);
            _database.SaveChanges();
        }
    }
}
