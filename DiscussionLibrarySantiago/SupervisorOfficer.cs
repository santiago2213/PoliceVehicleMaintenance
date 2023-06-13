using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DiscussionLibrarySantiago
{
    public class SupervisorOfficer
    {
        [Key]
        public int SupervisorOfficerId { get; set; }



        //Relational database connections (Foreign Keys)
        public string SupervisorId { get; set; }
        public string OfficerId { get; set; }



        //Object-oriented connections
        [ForeignKey("SupervisorId")]
        public Supervisor Supervisor { get; set; }

        [ForeignKey("OfficerId")]
        public Officer Officer { get; set; }


        //Any other properties required
        public DateTime DateSupervisionStarted { get; set; }
        public DateTime? DateSupervisionEnded { get; set; }


        public SupervisorOfficer(string supervisorId, string officerId, DateTime dateSupervisionStarted)
        {
            SupervisorId = supervisorId;    
            OfficerId = officerId;
            DateSupervisionStarted = dateSupervisionStarted;
        }

        public SupervisorOfficer()
        {

        }


    }
}