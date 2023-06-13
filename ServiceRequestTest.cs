using DiscussionLibrarySantiago;
using DiscussionMvcSantiago.Controllers;
using DiscussionMvcSantiago.Models;
using DiscussionMvcSantiago.Services;
using DiscussionMvcSantiago.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Moq;
using System.Runtime.Intrinsics;

namespace DiscussionTestSantiago
{
    
    public class ServiceRequestTest
    {
        Mock<IServiceRequestRepo> mockServiceRequestRepo = new Mock<IServiceRequestRepo>();
        Mock<IAppUserRepo> mockAppUserRepo = new Mock<IAppUserRepo>();
        Mock<IVehicleRepo> mockVehicleRepo = new Mock<IVehicleRepo>();
        Mock<IServiceRequestEmailSender> mockEmailSender = new Mock<IServiceRequestEmailSender>();

        ServiceRequestController serviceRequestController;

        public ServiceRequestTest()
        {
            mockServiceRequestRepo = new Mock<IServiceRequestRepo>();
            mockAppUserRepo = new Mock<IAppUserRepo>();
            mockVehicleRepo = new Mock<IVehicleRepo>();
            mockEmailSender = new Mock<IServiceRequestEmailSender>();
            serviceRequestController = new ServiceRequestController(mockServiceRequestRepo.Object, mockAppUserRepo.Object, mockVehicleRepo.Object, mockEmailSender.Object);
        }


        [Fact]
        public void ShouldMakeServiceRequest()
        {
            mockEmailSender.Setup(m => m.SendServiceRequestEmail("mockControllerandMethodName", "mockEmail", "mockSubject", "mockMessage"));

            string mockOfficerId = "O001";
            mockAppUserRepo.Setup(m => m.GetUserId()).Returns(mockOfficerId);

            List<Vehicle> mockVehicles = CreateMockVehicles();
            mockVehicleRepo.Setup(m => m.GetAllVehicles()).Returns(mockVehicles);
            SupervisorOfficer supervisorOfficer = new SupervisorOfficer("1", mockOfficerId, new DateTime(2022,1,1));
            mockAppUserRepo.Setup(s => s.GetOfficerSupervisors(mockOfficerId)).Returns(supervisorOfficer);

            Supervisor mockSupervisor = new Supervisor { Id = supervisorOfficer.SupervisorId };
            mockAppUserRepo.Setup(m => m.GetSupervisor(mockSupervisor.Id)).Returns(mockSupervisor);

            //Arrange
            MakeServiceRequestViewModel viewModel = new MakeServiceRequestViewModel
            {
                Description = "Change Tires",
                VehicleId = 101
            };

            mockServiceRequestRepo.Setup(m => m.HasRequestBeenMade(viewModel.VehicleId))
                .Returns(false);

            int mockServiceRequestId = 1;
            ServiceRequest mockServiceRequest = null;

            mockServiceRequestRepo.Setup(m => m.MakeServiceRequest(It.IsAny<ServiceRequest>()))
                .Returns(mockServiceRequestId)
                .Callback<ServiceRequest>(addedServiceRequest => mockServiceRequest = addedServiceRequest);

            //Act
            serviceRequestController.MakeServiceRequest(viewModel);


            //Assert 
            Assert.Equal(mockOfficerId, mockServiceRequest.OfficerId);
        }

        [Fact]
        public void ShouldNOTMakeServiceRequest()
        {
            //mockVehicleRepo.Setup(m => m.GetAllVehicles()).Returns(new List<Vehicle>());

            List<Vehicle> mockVehicles = CreateMockVehicles();
            mockVehicleRepo.Setup(m => m.GetAllVehicles()).Returns(mockVehicles);

            string mockOfficerId = "O1";
            mockAppUserRepo.Setup(m => m.GetUserId()).Returns(mockOfficerId);

            MakeServiceRequestViewModel viewModel = new MakeServiceRequestViewModel
            {
                VehicleId = 1,
                Description = "Test"
            };

            mockServiceRequestRepo.Setup(m => m.HasRequestBeenMade(viewModel.VehicleId)).Returns(true);

            int mockServiceRequestId = 101;
            ServiceRequest mockServiceRequest = null;
            mockServiceRequestRepo.Setup(m => m.MakeServiceRequest(It.IsAny<ServiceRequest>())).Returns(mockServiceRequestId);
        }




        [Fact]
        public void ShouldSearchForCompletedServiceRequestsByVehicle()
        {

            List<ServiceRequest> mockServiceRequestData = CreateMockData();
            mockServiceRequestRepo.Setup(m => m.GetAllServiceRequests()).Returns(mockServiceRequestData);

            List<Vehicle> mockVehicles = CreateMockVehicles();
            mockVehicleRepo.Setup(m => m.GetAllVehicles()).Returns(mockVehicles);

            List<Mechanic> mockMechanics = new List<Mechanic>();
            mockAppUserRepo.Setup(m => m.GetAllMechanics()).Returns(mockMechanics);

            int expectedNumberOfServiceRequests = 2;

            //ServiceRequestController serviceRequestController = new ServiceRequestController(mockServiceRequestRepo.Object);

            string inputSearchVin = null;
            

            SearchServiceRequestsViewModel viewModel = new SearchServiceRequestsViewModel
            {
                //SearchVehicle = null,
                SearchVIN = "V001",
                //SearchDateServiceRequested = null,
                //SearchDescription = null,
                SearchMechanicFullName = null,
                //SearchOfficer = null,
                //SearchVehicleId = 1
            };


            ViewResult viewResult = serviceRequestController.SearchServiceRequests(viewModel) as ViewResult;

            SearchServiceRequestsViewModel resultModel = viewResult.Model as SearchServiceRequestsViewModel;

            List<ServiceRequest> actualResultList = resultModel.Result;
            int actualNumberOfServiceRequests = actualResultList.Count;

            Assert.Equal(expectedNumberOfServiceRequests, actualNumberOfServiceRequests);

        }

        [Fact]
        public void ShouldSearchForServiceRequestsWithNotesByMechanic()
        {


            List<ServiceRequest> mockServiceRequestData = CreateMockData();
            mockServiceRequestRepo.Setup(m => m.GetAllServiceRequests()).Returns(mockServiceRequestData);

            List<Vehicle> mockVehicles = CreateMockVehicles();
            mockVehicleRepo.Setup(m => m.GetAllVehicles()).Returns(mockVehicles);

            List<Mechanic> mockMechanics = new List<Mechanic>();
            mockAppUserRepo.Setup(m => m.GetAllMechanics()).Returns(mockMechanics);

            int expectedNumberOfServiceRequests = 1;

            //ServiceRequestController serviceRequestController = new ServiceRequestController(mockServiceRequestRepo.Object);

            string? inputSearchVin = null;
            string? inputSearchMechanicName = "First mechanic";
            //string? inputSearchMechanicNotes = "New Tires Needed";
            DateTime? inputSearchDateCompleted = null;

            SearchServiceRequestsViewModel viewModel = new SearchServiceRequestsViewModel
            {
                //SearchVehicle = null,
                SearchVIN = null,
                //SearchDateServiceRequested = null,
                //SearchDescription = null,
                SearchMechanicFullName = inputSearchMechanicName,
                //SearchOfficer = null,
                //SearchVehicleId = null
            };

            ViewResult viewResult = serviceRequestController.SearchServiceRequests(viewModel) as ViewResult;

            SearchServiceRequestsViewModel resultModel = viewResult.Model as SearchServiceRequestsViewModel;

            List<ServiceRequest> actualResultList = resultModel.Result;

            int actualNumberOfServiceRequests = actualResultList.Count;

            Assert.Equal(expectedNumberOfServiceRequests, actualNumberOfServiceRequests);

        }


        public List<Vehicle> CreateMockVehicles() 
        { 
            List<Vehicle> mockVehicles = new List<Vehicle>();

            Vehicle vehicle = new Vehicle("V001", new DateTime(2020, 1, 1), 1, 1, 2021, 12000, "S1") { VehicleId = 1};
            mockVehicles.Add(vehicle);

            vehicle = new Vehicle("V002", new DateTime(2020, 1, 1), 1, 1, 2021, 12000, "S1") { VehicleId = 2 };
            mockVehicles.Add(vehicle);

            vehicle = new Vehicle("V003", new DateTime(2020, 1, 1), 1, 1, 2021, 12000, "S2") { VehicleId = 3 };
            mockVehicles.Add(vehicle);

            return mockVehicles;
        }


        //1. Mock Data (DatePurchased)
        public List<ServiceRequest> CreateMockData()
        {
            List<ServiceRequest> mockData = new List<ServiceRequest>();

            List<Notes> mockNotes = new List<Notes>();

            string description = "Check Tires";
            string vin = "V001";
            string officerName = "First officer";
            DateTime dateServiceRequested = new DateTime(2022, 10, 1);
            string supervisorName = "First supervisor";
            DateTime dateDecided = new DateTime(2022, 10, 2);
            string mechanicName = "First mechanic";
            int vehicleId = 1;
            string officerId = "O1";
            
            DateTime dateServiced = new DateTime(2022, 10, 6);
            Vehicle vehicle = new Vehicle { VIN = vin, DatePurchased = new DateTime(2022, 10, 28), VehicleId = vehicleId};
            Officer officer = new Officer { Fullname = officerName };
            Mechanic mechanic = new Mechanic { Fullname = mechanicName };
            Supervisor supervisor = new Supervisor { Fullname = supervisorName };
            ServiceRequest serviceRequest = new ServiceRequest(description, vehicleId, officerId) {Mechanic = mechanic, Vehicle = vehicle };

            Notes notes = new Notes { ServiceRequest = serviceRequest, Description = "New tires needed", NoteCreated = new DateTime(2022, 10, 5), Mechanic = mechanic };
            mockNotes.Add(notes);
            notes = new Notes{ ServiceRequest = serviceRequest, Description = "Change tires", Supervisor = supervisor, NoteCreated = new DateTime(2022, 10, 6), Mechanic = mechanic };
            mockNotes.Add(notes);

            serviceRequest.ServiceRequestNotes = mockNotes;

            mockData.Add(serviceRequest);



            description = "Check Brakes";
            vin = "V001";
            officerName = "First officer";
            dateServiceRequested = new DateTime(2022, 10, 17);
            supervisorName = "First supervisor";
            dateDecided = new DateTime(2022, 10, 17);
            vehicle = new Vehicle { VIN = vin, DatePurchased = new DateTime(2022, 10, 28), VehicleId = vehicleId };
            officer = new Officer { Fullname = officerName };
            supervisor = new Supervisor { Fullname = supervisorName };
            serviceRequest = new ServiceRequest(description, 2, officerId) {Vehicle = vehicle };

            mockData.Add(serviceRequest);


            description = "Oil change";
            vin = "V002";
            officerName = "Second officer";
            dateServiceRequested = new DateTime(2022, 10, 16);
            supervisorName = "Second supervisor";
            mechanicName = "First mechanic";
            dateDecided = new DateTime(2022, 10, 17);
            vehicle = new Vehicle { VIN = vin, DatePurchased = new DateTime(2022, 10, 28), VehicleId = vehicleId };
            officer = new Officer { Fullname = officerName };
            mechanic = new Mechanic { Fullname = mechanicName };
            supervisor = new Supervisor { Fullname = supervisorName };
            serviceRequest = new ServiceRequest(description, 3, officerId) { Vehicle = vehicle };

            mockData.Add(serviceRequest);


            return mockData;
        }



    }
}
