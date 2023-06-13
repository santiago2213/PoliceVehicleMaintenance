using DiscussionLibrarySantiago;
using DiscussionMvcSantiago.Controllers;
using DiscussionMvcSantiago.Models;
using DiscussionMvcSantiago.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Runtime.Intrinsics;
using Xunit.Sdk;

namespace DiscussionTestSantiago
{
    public class VehicleTest
    {
        private Mock<IVehicleRepo> mockVehicleRepo;
        private Mock<IAppUserRepo> mockAppUserRepo;
        private VehicleController vehicleController;

        public VehicleTest()
        {
            mockVehicleRepo = new Mock<IVehicleRepo>();
            mockAppUserRepo= new Mock<IAppUserRepo>();
            vehicleController = new VehicleController(mockVehicleRepo.Object, mockAppUserRepo.Object);
        }


        [Fact]
        public void ShouldFindLastDateServiced()
        {
            //Arrange
            List<ServiceRequest> serviceRequests= new List<ServiceRequest>();
            ServiceRequest serviceRequest = new ServiceRequest("Change Tires", 1, "O1") { DateServiced = new DateTime(2023, 3, 1) };
            serviceRequests.Add(serviceRequest);
            Vehicle vehicle= new Vehicle() {VehicleServiceRequests = serviceRequests};

            string expected = serviceRequest.DateServiced.Value.ToShortDateString();

            //Act
            string actual = vehicle.FindLastDateServiced();


            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void ShouldAddVehicle()
        {
            string mockSupervisorId = "S001";
            mockAppUserRepo.Setup(m => m.GetUserId()).Returns(mockSupervisorId);

            //AAA
            //1. Arrange
            //Create ViewModel
            AddVehicleViewModel viewModel = new AddVehicleViewModel { VIN = "V001", 
                DatePurchased = DateTime.Now.AddYears(-1), 
                VehicleMakeId = 1, VehicleModelId = 13, 
                Year = 2022, 
                Mileage = 1000 
            };

            int mockVehicleId = 101;
            Vehicle mockVehicle = null;
            //object created in controller method
            //provided back to test method
            //calling bacj tge Vehilce object created on the controller method
            //addedVehicle
            //assigning to mockVehicle
            mockVehicleRepo.Setup(m => m.AddVehicle(It.IsAny<Vehicle>()))
                .Returns(mockVehicleId)
                .Callback<Vehicle>(addedVehicle => mockVehicle = addedVehicle);

            //2. Act
            vehicleController.AddVehicle(viewModel);

            //3. Assert
            //a. Simple test  //fluent notation
            mockVehicleRepo.Verify(m => m.AddVehicle(It.IsAny<Vehicle>()), Times.Once(), "Add Vehicle not called once");

            //b. Rigorous test (Test whether the vehicle object was created)
            //Expected v Actual
            Assert.NotNull(mockVehicle);
            //string expectedVin, viewModel.VIN
            //Assert.Equal(expectedVin, viewModel.Vin)
            Assert.Equal(viewModel.VIN, mockVehicle.VIN);
            Assert.Equal(VehicleStatusOptions.Running, mockVehicle.VehicleStatus);
            Assert.Equal(mockSupervisorId, mockVehicle.VehicleAddedBySupervisorId);
            

        }


        [Fact]  //Fail quicly (Red ,Green) -Test Drien Dev
        public void ShouldCalculateVehicleAge()
        {
            //N Code:
            //DateTime dateTimeForTest = new DateTime(2012, 09, 08);
            //Vehicle v1 = new Vehicle(1, "1a23b", dateTimeForTest);

            //VehiclePurchaseDate: 9/8/2012
            //What I expect
            int expectedVehicleAge = 10;
            int actualVehicleAge = 0;

            //Call CalculateAge() - Object oriented method
            //Need an object (Construct the object)
            //Create a constructor
            string vin = "V001";
            DateTime datePurchased = DateTime.Now.AddYears(-10);   //new DateTime(2013, 8, 9);
            Vehicle vehicle = new Vehicle(vin, datePurchased);
            actualVehicleAge = vehicle.CalculateAge();


            Assert.Equal(expectedVehicleAge, actualVehicleAge);

        }
        

        [Fact]
        public void ShouldFindVehicleRetirementStatusRetired()
        {
            VehicleStatusOptions expectedVehicleRetirementStatus = VehicleStatusOptions.Retired;
            VehicleStatusOptions actualVehicleRetirementStatus;

            string vin = "JA32U2FU2EU016914";
            DateTime datePurchased = new DateTime(2000, 9, 15);
            int mileage = 150000;

            Vehicle vehicle = new Vehicle(vin, datePurchased, mileage);

            actualVehicleRetirementStatus = vehicle.FindVehicleRetirementStatus();
            Assert.Equal(expectedVehicleRetirementStatus, actualVehicleRetirementStatus);
        }

        [Fact]
        public void ShouldFindVehicleRetirementStatusActive()
        {
            VehicleStatusOptions expectedVehicleRetirementStatus = VehicleStatusOptions.Running;
            VehicleStatusOptions actualVehicleRetirementStatus;

            string vin = "JA32U2FU2EU016914";
            DateTime datePurchased = new DateTime(2020, 9, 15);
            int mileage = 150000;

            Vehicle vehicle = new Vehicle(vin, datePurchased, mileage);

            actualVehicleRetirementStatus = vehicle.FindVehicleRetirementStatus();
            Assert.Equal(expectedVehicleRetirementStatus, actualVehicleRetirementStatus);
        }



        [Fact]
        public void ShouldSearchForVehicleByMileage()
        {
            //AAA Testing

            //1. Arrange (Setting up)
            //Mock the interface
            //Mock<IVehicleRepo> mockVehicleRepo = new Mock<IVehicleRepo>();

            List<Vehicle> mockVehicleData = CreateMockData();
            // LINQ, Lambda Expressions
            mockVehicleRepo.Setup(m => m.GetAllVehicles()).Returns(mockVehicleData);

            List<VehicleMake> mockVehicleMake = CreateMockVehicleMakes();
            mockVehicleRepo.Setup(m => m.GetAllVehicleMakes()).Returns(mockVehicleMake);

            List<VehicleModel> mockVehicleModels = CreateMockVehicleModels();
            mockVehicleRepo.Setup(m => m.GetAllVehicleModels()).Returns(mockVehicleModels);

            //>= 150,000 miles
            int expectedNumberOfVehicles = 2;

            //VehicleController vehicleController = new VehicleController(mockVehicleRepo.Object);
            //Casting: Dev knows for sure the data type .NET has multiple options

            //2. Act (Exercise the method with the logic)
            SearchVehiclesViewModel viewModel = new SearchVehiclesViewModel { SearchMileage = 150000, SearchPurchaseDate = null, SearchServicedDate= null };
            int? inputSearchMileage = 150000;
            //DateTime? inputSearchPurchaseDate = null;

            ViewResult viewResult = vehicleController.SearchVehicles(viewModel) as ViewResult; // "=" assignment operator
            //Expected against actual
            SearchVehiclesViewModel resultModel = viewResult.Model as SearchVehiclesViewModel;
            List<Vehicle> actualResultList = resultModel.SearchResult;


            int actualNumberOfVehicles = actualResultList.Count;

            //3. Assert 
            Assert.Equal(expectedNumberOfVehicles, actualNumberOfVehicles);
        }

        [Fact]
        public void ShouldSearchForVehicleByMileageAndDatePurchased()
        {
            //5 or more years old and 150,000 or more miles
            //Mock<IVehicleRepo> mockVehicleRepo = new Mock<IVehicleRepo>();

            List<Vehicle> mockVehicleData = CreateMockData();
            mockVehicleRepo.Setup(m => m.GetAllVehicles()).Returns(mockVehicleData);

            List<VehicleMake> mockVehicleMake = CreateMockVehicleMakes();
            mockVehicleRepo.Setup(m => m.GetAllVehicleMakes()).Returns(mockVehicleMake);

            List<VehicleModel> mockVehicleModels = CreateMockVehicleModels();
            mockVehicleRepo.Setup(m => m.GetAllVehicleModels()).Returns(mockVehicleModels);

            int expectedNumberOfVehicles = 1;

            //VehicleController vehicleController = new VehicleController(mockVehicleRepo.Object);

            SearchVehiclesViewModel viewModel = new SearchVehiclesViewModel { SearchMileage = 150000, SearchPurchaseDate = new DateTime(2012, 1, 1), SearchServicedDate = null };
            int? inputSearchMileage = 150000;
            DateTime inputSearchPurchaseDate = new DateTime(2012, 1, 1);
            ViewResult viewResult = vehicleController.SearchVehicles(viewModel) as ViewResult; // "=" assignment operator

            SearchVehiclesViewModel resultModel = viewResult.Model as SearchVehiclesViewModel;

            List<Vehicle> actualResultList = resultModel.SearchResult;

            int actualNumberOfVehicles = actualResultList.Count;

            Assert.Equal(expectedNumberOfVehicles, actualNumberOfVehicles);

        }

        [Fact]
        public void ShouldSearchForVehicleByDateServiced()
        {
            //Mock<IVehicleRepo> mockVehicleRepo = new Mock<IVehicleRepo>();

            List<Vehicle> mockVehicleData = CreateMockData();
            mockVehicleRepo.Setup(m => m.GetAllVehicles()).Returns(mockVehicleData);

            List<VehicleMake> mockVehicleMake = CreateMockVehicleMakes();
            mockVehicleRepo.Setup(m => m.GetAllVehicleMakes()).Returns(mockVehicleMake);

            List<VehicleModel> mockVehicleModels = CreateMockVehicleModels();
            mockVehicleRepo.Setup(m => m.GetAllVehicleModels()).Returns(mockVehicleModels);

            int expectedNumberOfVehicles = 1;

            //VehicleController vehicleController = new VehicleController(mockVehicleRepo.Object);

            SearchVehiclesViewModel viewModel = new SearchVehiclesViewModel { SearchMileage = null, SearchPurchaseDate = null, SearchServicedDate = new DateTime(2022, 6, 1) };
            int? inputSearchMileage = null;
            DateTime? inputSearchPurchaseDate = null;
            DateTime inputSearchServiceDate = new DateTime(2022, 6, 1);
            ViewResult viewResult = vehicleController.SearchVehicles(viewModel) as ViewResult; // "=" assignment operator

            SearchVehiclesViewModel resultModel = viewResult.Model as SearchVehiclesViewModel;

            List<Vehicle> actualResultList = resultModel.SearchResult;

            int actualNumberOfVehicles = actualResultList.Count;

            Assert.Equal(expectedNumberOfVehicles, actualNumberOfVehicles);

        }


        //1. Mock Data (DatePurchased)
        public List<Vehicle> CreateMockData()
        {
            List<Vehicle> mockData = new List<Vehicle>();
            DateTime datePurchased = new DateTime(2011, 1, 1);
            Vehicle vehicle = new Vehicle { VIN = "V001", Mileage = 175000, DatePurchased = datePurchased };

            //dateServiceRequested;
            DateTime dateServiced = new DateTime(2022, 9, 1);
            ServiceRequest serviceRequest = new ServiceRequest("Windshield Replacement", 3, "O3");
            serviceRequest.Vehicle = vehicle; // Bottom Arrow
            vehicle.VehicleServiceRequests.Add(serviceRequest); //Top Arrow (More Efficient)

            mockData.Add(vehicle);

            dateServiced = new DateTime(2022, 3, 1);
            datePurchased = new DateTime(2011, 6, 6);
            vehicle = new Vehicle { VIN = "V002", Mileage = 149000, DatePurchased = datePurchased };
            serviceRequest = new ServiceRequest ("tire rotation", 2, "O2");
            serviceRequest.Vehicle = vehicle;
            vehicle.VehicleServiceRequests.Add(serviceRequest);

            mockData.Add(vehicle);

            dateServiced = new DateTime(2022, 4, 1);
            datePurchased = new DateTime(2013, 6, 6);
            vehicle = new Vehicle { VIN = "V003", Mileage = 150000, DatePurchased = datePurchased };
            serviceRequest = new ServiceRequest("Oil chagne", 1, "O1") { DateServiced = new DateTime(2023, 1, 1)};
            serviceRequest.Vehicle = vehicle;
            vehicle.VehicleServiceRequests.Add(serviceRequest);

            mockData.Add(vehicle);

            return mockData;
        }

        public List<VehicleMake> CreateMockVehicleMakes()
        {
            List<VehicleMake> mockVehicleMakes = new List<VehicleMake>();

            VehicleMake make = new VehicleMake { VehicleMakeName = "Subaru", VehicleMakeId = 1 };

            mockVehicleMakes.Add(make); 

            return mockVehicleMakes;
        }

        public List<VehicleModel> CreateMockVehicleModels()
        {
            List<VehicleModel> mockVehicleModels = new List<VehicleModel>();

            VehicleModel model = new VehicleModel { VehicleModelName = "Forrester", VehicleMakeId = 1, VehicleModelId = 1 };

            mockVehicleModels.Add(model);

            return mockVehicleModels;
        }



    }
}
