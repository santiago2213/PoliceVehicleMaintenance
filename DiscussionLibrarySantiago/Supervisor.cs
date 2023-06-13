using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscussionLibrarySantiago
{
    public class Supervisor : AppUser
    {
        public List<ServiceRequest> ServiceRequestsDecided { get; set; }
        public List<Notes>? SupervisorNotes { get; set; }
        public List<SupervisorOfficer> OfficersSupervised { get; set; }
        


        public Supervisor(string fullname, string email, string phone, string password)
            : base(fullname, email, phone, password)
        {
            ServiceRequestsDecided = new List<ServiceRequest>();
            SupervisorNotes = new List<Notes>();
            OfficersSupervised = new List<SupervisorOfficer>();
        }



        public Supervisor()
        {
            ServiceRequestsDecided = new List<ServiceRequest>();
            SupervisorNotes = new List<Notes>();
            OfficersSupervised = new List<SupervisorOfficer>();
        }

    }

    
}