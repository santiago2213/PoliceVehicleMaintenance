using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DiscussionLibrarySantiago
{
    public class Notes 
    {
        [Key]
        public int NoteId { get; set; }
        public string Description { get; set; }
        public DateTime? NoteCreated { get; set; }
        public string MechanicId { get; set; }
        [ForeignKey("MechanicId")]
        public Mechanic? Mechanic { get; set; }
        public string SupervisorId { get; set; }
        [ForeignKey("SupervisorId")]
        public Supervisor? Supervisor { get; set; }
        public int ServiceRequestId { get; set; }
        [ForeignKey("ServiceRequestId")]
        public ServiceRequest ServiceRequest { get; set; }

        public Notes(ServiceRequest serviceRequest, string description, DateTime noteCreated, Mechanic? mechanic, Supervisor? supervisor)
        {
            ServiceRequest = serviceRequest;
            Description = description;
            NoteCreated = noteCreated;
            Mechanic = mechanic;
            Supervisor = supervisor;
        }

        public Notes()
        {

        }

        public Notes(string v, int serviceRequestId, string mechanicId, object value)
        {
            ServiceRequestId = serviceRequestId;
            MechanicId = mechanicId;
        }
    }
}