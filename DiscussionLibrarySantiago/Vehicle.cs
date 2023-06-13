using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//DiscussionLibrarySantiago.Vehicle
namespace DiscussionLibrarySantiago
{
    //test to check
    public class Vehicle
    {
        public int? VehicleMakeId { get; set; }
        [ForeignKey("VehicleMakeId")]
        public VehicleMake? VehicleMake { get; set; }
        public int? VehicleModelId { get; set; }
        [ForeignKey("VehicleModelId")]
        public VehicleModel? VehicleModel { get; set; }

        public string? VehicleAddedBySupervisorId { get; set; }
        [ForeignKey("VehicleAddedBySupervisorId")]
        public Supervisor? VehicleAddedBySupervisor { get; set; }
        public DateTime? VehicleAddedDateTime { get; set; }

        //Attributes (MCV: Properties) - class member
        //accessModifier/Type  dataType  propertyName [get / set]
        //In MVC each class should have its own identifier (it will be an int)
        [Key]
        public int VehicleId { get; set; }
        //attribute or instance variavble or field
        //private int _vehicleID;
        public string VIN { get; set; }
        public DateTime DatePurchased { get; set; }

        //public string Make { get; set; }
        //public string Model { get; set; }
        //Two FKs: MakeId, ModelId

        public int Year { get; set; } //foreach() for dropdownlist
        public int? Mileage {get; set;}
        //Should we have a property?
        public VehicleStatusOptions VehicleStatus { get; set; }
        public List<ServiceRequest> VehicleServiceRequests { get; set; }    


        //Create unit test
        public string FindLastDateServiced()
        {
            string lastDate = null;
            lastDate = this.VehicleServiceRequests.Max(vsr => vsr.DateServiced).Value.ToShortDateString();

            return lastDate;
        }



        //Commit for 9/15
        public int CalculateAge() //no parameters needed
        {
            int age = DateTime.Now.Year - DatePurchased.Year;
            return age;
        }

        public VehicleStatusOptions FindVehicleRetirementStatus()
        {
            VehicleStatusOptions retirementstatus = CalculateAge() >= 20 || Mileage >= 200000 ? VehicleStatusOptions.Retired : VehicleStatusOptions.Running ;

            return retirementstatus;
        }

        //Method overloading
        public Vehicle()    //empty constructor required in MVC
        {                   //Object - Relational - Mapper (ORM)
            VehicleServiceRequests = new List<ServiceRequest>();                 //Entity Framework
        }

        public Vehicle(string vin, DateTime datePurchased, int vehicleMakeId, int vehicleModelId, int year, int? mileage = null, string? vehicleAddedBySupervisorId = null)
           // (string vin, DateTime datePurchased, int vehicleMakeId, int vehicleModelId, int year, int? mileage = null)
        {
            VIN = vin;
            DatePurchased = datePurchased;
            VehicleMakeId= vehicleMakeId;
            VehicleModelId = vehicleModelId;
            Year = year;
            Mileage = mileage;
            VehicleAddedBySupervisorId= vehicleAddedBySupervisorId;
            VehicleAddedDateTime= DateTime.Now;
            VehicleStatus = FindVehicleRetirementStatus();
            VehicleServiceRequests = new List<ServiceRequest>();
        }

        //Vehicle is retired if it is >= 20 years or mileage >= 200000 miles
        public Vehicle(string vin, DateTime datePurchased, int? mileage = null)
        {
            VIN = vin;
            Mileage = mileage;
            DatePurchased = datePurchased;
            VehicleStatus = FindVehicleRetirementStatus();
            VehicleServiceRequests = new List<ServiceRequest>();
        }//end of Vehicle Constructor

                          
    



    }//end of Vehicle class

    public enum VehicleStatusOptions
    {
        Running, BeingServiced, Retired
    }




}//end of namespace
