using DiscussionLibrarySantiago;
using System.Security.Policy;

namespace DiscussionMvcSantiago.Models
{
    public interface IAppUserRepo
    {
        public List<Officer> GetAllOfficers();
        public string GetUserId();
        public List<Mechanic> GetAllMechanics();
        public List<Supervisor> GetAllSupervisors();
        public void AddSupervisorOfficer(SupervisorOfficer supervisorOfficer);
        SupervisorOfficer GetOfficerSupervisors(string officerId);
        Supervisor GetSupervisor(string supervisorId);
        Officer GetOfficer(string officerId);
    }
}
