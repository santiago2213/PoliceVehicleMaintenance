using DiscussionLibrarySantiago;
using Microsoft.AspNetCore.Identity;

namespace DiscussionMvcSantiago.Data
{
    public class InitialDatabase
    {
        //class method (vs. instance / object method
        public static void SeedDatabase(IServiceProvider services)
        {
            //1. Database service
            ApplicationDbContext database  = services.GetRequiredService<ApplicationDbContext>();

            //2. App Users service
            UserManager<AppUser> userManager = services.GetRequiredService<UserManager<AppUser>>();

            //3. Roles service
            RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            string officerRole = "Officer";
            string supervisorRole = "Supervisor";
            string mechanicRole = "Mechanic";
         

      
            if(!database.Roles.Any()) //if Roles table is empty
            {
                IdentityRole role = new IdentityRole(officerRole);
                roleManager.CreateAsync(role).Wait();

                role = new IdentityRole(supervisorRole);
                roleManager.CreateAsync(role).Wait();

                role = new IdentityRole(mechanicRole);
                roleManager.CreateAsync(role).Wait();
            }


            if (!database.AppUser.Any())
            {
                AppUser appUser = new AppUser("Test Admin 1", "TestAdmin1@test.com", "3040000004", "Test.Admin1");
                appUser.EmailConfirmed = true;
                userManager.CreateAsync(appUser).Wait();
                List<string> allRoles = new List<string>();
                allRoles.Add(officerRole);
                allRoles.Add(supervisorRole);
                allRoles.Add(mechanicRole);
                userManager.AddToRolesAsync(appUser, allRoles).Wait();
            }



            if (!database.Officer.Any())
            {
                Officer officer = new Officer("Test Officer 1", "Test.Officer1@test.com", "3040000001", "Test.Officer1");
                officer.EmailConfirmed = true;
                userManager.CreateAsync(officer).Wait();
                userManager.AddToRoleAsync(officer, officerRole).Wait();
            }
            if (!database.Supervisor.Any())
            {
                Supervisor supervisor = new Supervisor("Test Supervisor 1", "Test.Supervisor1@test.com", "3040000002", "Test.Supervisor1");
                supervisor.EmailConfirmed = true;
                userManager.CreateAsync(supervisor).Wait();
                userManager.AddToRoleAsync(supervisor, supervisorRole).Wait();
            }
            if (!database.Mechanic.Any())
            {
                Mechanic mechanic = new Mechanic("Test Mechanic 1", "Test.Mechanic1@test.com", "3040000003", "Test.Mechanic1");
                mechanic.EmailConfirmed = true;
                userManager.CreateAsync(mechanic).Wait();
                userManager.AddToRoleAsync(mechanic, mechanicRole).Wait();
            }
            if (!database.SupervisorOfficer.Any())
            {
                string supervisorId = database.Supervisor.Where(s => s.UserName == "Test.Supervisor1@test.com").First().Id;
                string officerId = database.Officer.Where(s => s.UserName == "Test.Officer1@test.com").First().Id;
                SupervisorOfficer supervisorOfficer = new SupervisorOfficer(supervisorId, officerId, DateTime.Now);
                database.SupervisorOfficer.Add(supervisorOfficer);
                database.SaveChanges();
            }


            if (!database.VehicleMake.Any())
            {
                VehicleMake make = new VehicleMake("Subaru");
                database.VehicleMake.Add(make);
                database.SaveChanges();

                make = new VehicleMake("GM");
                database.VehicleMake.Add(make);
                database.SaveChanges();

                make = new VehicleMake("Honda");
                database.VehicleMake.Add(make);
                database.SaveChanges();

                make = new VehicleMake("Dodge");
                database.VehicleMake.Add(make);
                database.SaveChanges();

                make = new VehicleMake("Ford");
                database.VehicleMake.Add(make);
                database.SaveChanges();

                make = new VehicleMake("Toyota");
                database.VehicleMake.Add(make);
                database.SaveChanges();

                make = new VehicleMake("Chevrolet");
                database.VehicleMake.Add(make);
                database.SaveChanges();

                make = new VehicleMake("Jeep");
                database.VehicleMake.Add(make);
                database.SaveChanges();

                make = new VehicleMake("Ram");
                database.VehicleMake.Add(make);
                database.SaveChanges();

                make = new VehicleMake("GMC");
                database.VehicleMake.Add(make);
                database.SaveChanges();

                make = new VehicleMake("Nissan");
                database.VehicleMake.Add(make);
                database.SaveChanges();

                make = new VehicleMake("BMW");
                database.VehicleMake.Add(make);
                database.SaveChanges();
            }

            if (!database.VehicleModel.Any())
            {
                VehicleModel model = new VehicleModel("Forrester", 1);
                database.VehicleModel.Add(model);
                database.SaveChanges();

                model = new VehicleModel("Saturn", 2);
                database.VehicleModel.Add(model);
                database.SaveChanges();

                model = new VehicleModel("Accord", 3);
                database.VehicleModel.Add(model);
                database.SaveChanges();

                model = new VehicleModel("Challenger", 4);
                database.VehicleModel.Add(model);
                database.SaveChanges();

                model = new VehicleModel("Mustang", 5);
                database.VehicleModel.Add(model);
                database.SaveChanges();

                model = new VehicleModel("Camry", 6);
                database.VehicleModel.Add(model);
                database.SaveChanges();

                model = new VehicleModel("Silverado", 7);
                database.VehicleModel.Add(model);
                database.SaveChanges();

                model = new VehicleModel("Wrangler", 8);
                database.VehicleModel.Add(model);
                database.SaveChanges();

                model = new VehicleModel("1500", 9);
                database.VehicleModel.Add(model);
                database.SaveChanges();

                model = new VehicleModel("Sierra", 10);
                database.VehicleModel.Add(model);
                database.SaveChanges();

                model = new VehicleModel("Altima", 11);
                database.VehicleModel.Add(model);
                database.SaveChanges();

                model = new VehicleModel("3 Series", 12);
                database.VehicleModel.Add(model);
                database.SaveChanges();

                model = new VehicleModel("Legacy", 1);
                database.VehicleModel.Add(model);
                database.SaveChanges();
            }


            if (!database.Vehicle.Any()) //Is there any vehicle in this table? No!
            {
                Supervisor supervisor = database.Supervisor.Where(o => o.UserName == "Test.Supervisor1@test.com").First();
                string supervisorID = supervisor.Id;

                Vehicle vehicle = new Vehicle("V001", new DateTime(2020, 1, 1), 1, 1, 2021, 12000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();

                vehicle = new Vehicle("V002", new DateTime(1996, 12, 3), 2, 2, 1995, 195000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();

                vehicle = new Vehicle("V003", new DateTime(2010, 2, 14), 3, 3, 2010, 120000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();

                vehicle = new Vehicle("V004", new DateTime(2020, 6, 30), 4, 4, 2020, 5000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();

                vehicle = new Vehicle("V005", new DateTime(2019, 12, 1), 5, 5, 2019, 9000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();

                vehicle = new Vehicle("V006", new DateTime(2015, 8, 2), 6, 6, 2015, 90000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();

                vehicle = new Vehicle("V007", new DateTime(2018, 3, 15), 7, 7, 2018, 25000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();

                vehicle = new Vehicle("V008", new DateTime(2017, 11, 20), 8, 8, 2017, 18000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();

                vehicle = new Vehicle("V009", new DateTime(2016, 7, 5), 9, 9, 2016, 22000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();

                vehicle = new Vehicle("V010", new DateTime(2014, 12, 31), 10, 10, 2014, 120000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();

                vehicle = new Vehicle("V011", new DateTime(2013, 5, 1), 11, 11, 2013, 35000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();

                vehicle = new Vehicle("V012", new DateTime(2012, 8, 15), 12, 12, 2012, 45000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();

                vehicle = new Vehicle("V013", new DateTime(2014, 8, 15), 1, 13, 2014, 65000, supervisorID);
                database.Vehicle.Add(vehicle);
                database.SaveChanges();
            }


            if(!database.ServiceRequest.Any())
            {
                DateTime dateServiceRequested = new DateTime(2022, 10, 1);
                DateTime dateServiced = new DateTime(2022, 10, 5);
                Officer officer = database.Officer.Where(o => o.UserName == "Test.Officer1@test.com").First();
                string officerId = officer.Id;

                ServiceRequest serviceRequest = new ServiceRequest("Needs oil change", 1, officerId) {DateServiceRequested = dateServiceRequested, DateServiced = dateServiced };
                database.ServiceRequest.Add(serviceRequest);
                database.SaveChanges();


                //Service request on 11/1/2022 for second vehicle 11/5/2022
                dateServiceRequested = new DateTime(2022, 11, 1);
                dateServiced = new DateTime(2022, 11, 5);
                officerId = officer.Id;

                serviceRequest = new ServiceRequest("Needs new tires", 2, officerId) { DateServiceRequested = dateServiceRequested, DateServiced = dateServiced };
                database.ServiceRequest.Add(serviceRequest);
                database.SaveChanges();
            }

            if(!database.SupervisorOfficer.Any())
            {
                Officer officer = database.Officer.Where(o => o.UserName == "Test.Officer1@test.com").First();
                string officerId = officer.Id;

                Supervisor supervisor = database.Supervisor.Where(o => o.UserName == "Test.Supervisor1@test.com").First();
                string supervisorId = supervisor.Id;

                SupervisorOfficer supervisorOfficer = new SupervisorOfficer(supervisorId, officerId, new DateTime(2023, 3, 14));
                database.SupervisorOfficer.Add(supervisorOfficer);
                database.SaveChanges();
            }

           
         
            
        }
    }
}
