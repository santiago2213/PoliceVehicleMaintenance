using DiscussionLibrarySantiago;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiscussionMvcSantiago.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //Translator from classes to tables: Entity Framework EF (O R M)
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Officer> Officer { get; set; }
        public DbSet<Supervisor> Supervisor { get; set; }
        public DbSet<Mechanic> Mechanic { get; set; }
        public DbSet<SupervisorOfficer> SupervisorOfficer { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<ServiceRequest> ServiceRequest { get; set; }
        public DbSet<Notes> Note { get; set; }
        public DbSet<VehicleMake> VehicleMake { get; set; }
        public DbSet<VehicleModel> VehicleModel { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Vehicle>()
                .HasAlternateKey( v => v.VIN);

            builder.Entity<ServiceRequest>()
                .HasAlternateKey(sr => new {sr.VehicleId, sr.DateServiceRequested});
        }
    }
}