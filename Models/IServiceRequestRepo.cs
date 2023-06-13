using DiscussionLibrarySantiago;

namespace DiscussionMvcSantiago.Models
{
    public interface IServiceRequestRepo   //What should be done, Not HOW
    {

        public List<ServiceRequest> GetAllServiceRequests();
        bool HasRequestBeenMade(int vehicleId);
        public int MakeServiceRequest(ServiceRequest serviceRequest);
        public List<ServiceRequest> GetAllUndecidedRequestsForSupervisor(string supervisorId);
        public ServiceRequest GetServiceRequest(int serviceRequestId);
        void UpdateServiceRequest(ServiceRequest serviceRequest);
        public List<ServiceRequest> GetAllUnservicedRequestsForMechanic(string mechanicId);
        public void AddNote(Notes note);
    }

 
}
