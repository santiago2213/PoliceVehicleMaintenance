using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscussionLibrarySantiago
{
    public class Mechanic : AppUser
    {
 
        public List<ServiceRequest> ServiceRequestWorkedOn{ get; set; }

        public List<Notes> MechanicNotes { get; set; }




        public Mechanic(string fullname, string email, string phone, string password)
            : base(fullname, email, phone, password)
        {
            ServiceRequestWorkedOn = new List<ServiceRequest>();
            MechanicNotes = new List<Notes>();
        }






        public Mechanic()
        {
            ServiceRequestWorkedOn = new List<ServiceRequest>();
            MechanicNotes = new List<Notes>();  
        }

        


    }
}