using DiscussionLibrarySantiago;
using DiscussionMvcSantiago.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscussionMvcSantiago.Models
{
    public class ServiceRequestRepo : IServiceRequestRepo
    {
        private ApplicationDbContext _database;

        public ServiceRequestRepo(ApplicationDbContext database) 
        {
            _database = database;
        } 

        public List<ServiceRequest> GetAllUndecidedRequestsForSupervisor(string supervisorId)
        {
            List<ServiceRequest> allRequests = GetAllServiceRequests();
            List<ServiceRequest> undecidedRequestsForSupervisor =
                allRequests
                .Where(r => r.SupervisorId == supervisorId
                && r.SupervisorDecision == null).ToList();
            return undecidedRequestsForSupervisor;
        }

        public List<ServiceRequest> GetAllServiceRequests()
        {
            List<ServiceRequest> serviceRequests = _database.ServiceRequest
                .Include(sr => sr.Vehicle.VehicleMake)
                .Include(sr => sr.Vehicle.VehicleModel)
                .Include(sr => sr.Officer)
                .Include(sr => sr.Supervisor)
                .Include(sr => sr.Mechanic)
                .Include(sr => sr.ServiceRequestNotes)
                .ToList();
            return serviceRequests;
        }

        public bool HasRequestBeenMade(int vehicleId)
        {
            bool found = _database.ServiceRequest
                .Where(sr => sr.VehicleId == vehicleId 
                && sr.DateServiceRequested.Date == DateTime.Now.Date).Any(); //.FirstOrDefault();
            return found;
        }

        public int MakeServiceRequest(ServiceRequest serviceRequest)
        {
            _database.ServiceRequest.Add(serviceRequest);
            _database.SaveChanges();

            int serviceRequestId = serviceRequest.ServiceRequestId;
            return serviceRequestId;
        }

        public ServiceRequest GetServiceRequest(int serviceRequestId)
        {
            return _database.ServiceRequest.Find(serviceRequestId);
        }

        public void UpdateServiceRequest(ServiceRequest serviceRequest)
        {
            _database.ServiceRequest.Update(serviceRequest);
            _database.SaveChanges();
        }

        public List<ServiceRequest> GetAllUnservicedRequestsForMechanic(string mechanicId)
        {
            List<ServiceRequest> allRequests = GetAllServiceRequests();
            List<ServiceRequest> unservicedRequestsForMechanic =
                allRequests
                .Where(r => r.MechanicId == mechanicId
                && r.SupervisorDecision == SupervisorDecisionOptions.inservice).ToList();
            return unservicedRequestsForMechanic;
        }

        public void AddNote(Notes note)
        {
            _database.Note.Add(note);
            _database.SaveChanges();
        }

        public int MakeSupervisorResponseToRequests(Notes serviceRequestResponse, ServiceRequest serviceRequest)
        {
            _database.Note.Add(serviceRequestResponse);
            _database.SaveChanges();

            int serviceRequestId = serviceRequest.ServiceRequestId;
            return serviceRequestId;
        }
    }
}
