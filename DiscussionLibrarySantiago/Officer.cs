using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscussionLibrarySantiago
{
    public class Officer : AppUser
    {


        public List<ServiceRequest> ServiceRequestsSubmitted { get; set; }
        public List<SupervisorOfficer> Supervisors { get; set; }

        public Officer(string fullname, string email, string phone, string password)
            : base(fullname, email, phone, password)
        {
            
            ServiceRequestsSubmitted = new List<ServiceRequest>();
            Supervisors = new List<SupervisorOfficer>();
        }
                
        public Officer()
        {
            ServiceRequestsSubmitted = new List<ServiceRequest>();
            Supervisors = new List<SupervisorOfficer>();
        }

        
    }
}