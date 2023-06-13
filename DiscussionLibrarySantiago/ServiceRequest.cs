using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DiscussionLibrarySantiago
{
    public class ServiceRequest
    {
        [Key]
        public int ServiceRequestId { get; set; }
        public string Description { get; set; }
        public DateTime DateServiceRequested { get; set; }
        public DateTime? DateServiced { get; set; }
        public int VehicleId { get; set; } //RDB Connection
        [ForeignKey("VehicleId")] // Message to the Entity Framework to NOT create another FK
        public Vehicle Vehicle { get; set; } //OO Connection
        public List<Notes> ServiceRequestNotes { get; set; }
        public string? MechanicId { get; set; }
        [ForeignKey("MechanicId")]
        public Mechanic? Mechanic { get; set; }
        public string OfficerId { get; set; }
        [ForeignKey("OfficerId")]
        public Officer Officer { get; set; }
        public string? SupervisorId { get; set; }
        [ForeignKey("SupervisorId")]
        public Supervisor? Supervisor { get; set; }
        //public SupervisorDecisionOptions? SupervisorDecision { get; set; }
        public DateTime? DateSupervisorDecision { get; set; }
        public SupervisorDecisionOptions? SupervisorDecision { get; set; }

        public int? SupervisorOfficerId { get; set; }
        [ForeignKey("SupervisorOfficerId")]
        public SupervisorOfficer? ServiceRequestAddedByOfficer { get; set; }
        public DateTime? ServiceRequestAddedDateTime { get; set; }


        public ServiceRequest(string description, int vehicleId, string officerId)
        {
            this.Description = description;
            this.VehicleId = vehicleId;
            this.OfficerId = officerId;
            this.DateServiceRequested = DateTime.Now;
            this.ServiceRequestNotes = new List<Notes>();
        }

        //public ServiceRequest(//int serviceRequestId,
        //    DateTime dateServiceRequested, DateTime dateServiced)
        //{
        //    //ServiceRequestId = serviceRequestId;
        //    DateServiceRequested = dateServiceRequested;
        //    DateServiced = dateServiced;
        //    ServiceRequestNotes = new List<Notes>();    
        //}

        //public ServiceRequest(DateTime dateServiceRequested, DateTime dateServiced, string officerId)
        //{
        //    DateServiceRequested = dateServiceRequested;
        //    DateServiced = dateServiced;
        //    string officerID;
        //    ServiceRequestNotes = new List<Notes>();
        //}


        ////create constructor to meet business requirements
        //public ServiceRequest(DateTime dateServiceRequested, DateTime dateServiced, string description, string officerId, int vehicleId)
        //{
        //    DateServiceRequested = dateServiceRequested;
        //    DateServiced = dateServiced;
        //    Description = description;
        //    OfficerId = officerId;
        //    VehicleId = vehicleId;
        //}



        public ServiceRequest() //empty constructor (for ?)
        {
            ServiceRequestNotes = new List<Notes>();
        }

    }

    public enum SupervisorDecisionOptions
    {
        approved, denied, inservice, complete

    }

}
