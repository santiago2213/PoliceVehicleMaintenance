using DiscussionLibrarySantiago;
using DiscussionMvcSantiago.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DiscussionMvcSantiago.Models
{
    public class AppUserRepo : IAppUserRepo
    {
        private ApplicationDbContext _database;
        private IHttpContextAccessor _contextAccessor;

        public AppUserRepo(IHttpContextAccessor contextAccessor, ApplicationDbContext dbContext)
        { 
            _contextAccessor= contextAccessor;
            _database = dbContext;
        }

        public string GetUserId()
        {
            string userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }

        public List<Officer> GetAllOfficers()
        {
            List<Officer> officers = _database.Officer.ToList();
            return officers;
        }

        public List<Mechanic> GetAllMechanics()
        {
            List<Mechanic> mechanics = _database.Mechanic.ToList();
            return mechanics;
        }

        public List<Supervisor> GetAllSupervisors()
        {
            List<Supervisor> supervisors = _database.Supervisor.ToList();
            return supervisors;
        }

        public void AddSupervisorOfficer(SupervisorOfficer supervisorOfficer)
        {
            _database.SupervisorOfficer.Add(supervisorOfficer);
            _database.SaveChanges();

        }

        public SupervisorOfficer GetOfficerSupervisors(string officerId)
        {
            SupervisorOfficer supervisorOfficer =
                _database.SupervisorOfficer.Include(so => so.Supervisor)
                    .Where(so => so.OfficerId == officerId && so.DateSupervisionEnded == null)
                    .FirstOrDefault();
            return supervisorOfficer;
        }

        public Supervisor GetSupervisor(string supervisorId)
        {
            Supervisor supervisor = _database.Supervisor.Find(supervisorId);
            return supervisor;
        }

        public Officer GetOfficer(string officerId)
        {
            Officer officer = _database.Officer.Find(officerId);
            return officer;
        }
    }
}
