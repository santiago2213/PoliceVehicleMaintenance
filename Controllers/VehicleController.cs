using DiscussionLibrarySantiago;
using DiscussionMvcSantiago.Models;
using DiscussionMvcSantiago.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace DiscussionMvcSantiago.Controllers
{
    public class VehicleController : Controller     // : inherits all functionality of parent class
    {

        //Injecting IVehicleRepo into VehicleController
        //Instance Variable
        private IVehicleRepo _vehicleRepo;
        private IAppUserRepo _appUserRepo;


        public VehicleController(IVehicleRepo vehicleRepo, IAppUserRepo appUserRepo)
        {
            _vehicleRepo = vehicleRepo;
            _appUserRepo = appUserRepo; 
        }

        //Search, Add, Update - get, post
        //Delete - post? confirm. (probebly needs get and post)

        [HttpGet]
        public IActionResult EditVehicle(int vehicleId)
        {
            CreateDropDownLists();
            Vehicle vehicle = _vehicleRepo.FindVehicle(vehicleId);
            return View(vehicle);
        }

        [HttpPost]
        public IActionResult EditVehicle(Vehicle modifiedVehicle)
        {
            if (ModelState.IsValid)
            {
                _vehicleRepo.UpdateVehicle(modifiedVehicle);
                return RedirectToAction("SearchVehicles");
            }
            else
            {
                CreateDropDownLists();
                return View(modifiedVehicle);
            }


            return View();
        }

        [HttpGet]
        public IActionResult AddVehicle()
        {
            CreateDropDownLists();
            return View();
        }

        [HttpPost]
        public IActionResult AddVehicle(AddVehicleViewModel viewModel)
        {
            string supervisorId = _appUserRepo.GetUserId();
                //User.FindFirstValue(ClaimTypes.NameIdentifier);


            if(ModelState.IsValid)
            {
                Vehicle vehicle = new Vehicle(viewModel.VIN, 
                    viewModel.DatePurchased.Value, 
                    viewModel.VehicleMakeId.Value, 
                    viewModel.VehicleModelId.Value, 
                    viewModel.Year.Value, 
                    viewModel.Mileage.HasValue ? viewModel.Mileage.Value : null, 
                    supervisorId);

                int vehicleId = _vehicleRepo.AddVehicle(vehicle);
                if(vehicleId == -1)
                {
                    ModelState.AddModelError("VINRepeatedError", "VIN cannot be repeated");
                    CreateDropDownLists();
                    return View(viewModel);
                }

                return RedirectToAction("SearchVehicles");
            }
            else //when there are errors
            {
                CreateDropDownLists();
                return View(viewModel);
            }
            
        }

        public JsonResult GetModelsForMake(int? makeId)
        {
            //List<Vehicle> allVehicles = _vehicleRepo.GetAllVehicles();
            List<VehicleModel> allVehicleModels = _vehicleRepo.GetAllVehicleModels();

            if (makeId != null)
            {
                allVehicleModels = 
                    allVehicleModels.Where(v => v.VehicleMakeId == makeId).ToList();
            }

            //List<int> selectedModels = allVehicles.Select(v => v.VehicleModelId).Distinct().ToList();


            return Json(new SelectList(allVehicleModels, 
                "VehicleModelId", "VehicleModelName"));
        }

        public void CreateDropDownLists()
        {
            //List<VehiclMake> allVehicles = _vehicleRepo.GetAllVehicles();
            //List<VehicleModel> allVehicleMakes = _vehicleRepo.GetAllVehicleMakes();

            //get just the make names
            //var allMakes = allVehicles.Select(v => v.VehicleMake).Distinct().ToList();

            //make a SelsctList for the dropdownlist in the view
            //List<SelectListItem> listOfAllMakes = allMakes.Select(VehicleMake => new SelectListItem { Text = eachMake, Value = eachMake }).ToList();

            //ViewData sends the selectlist to the view
            ViewData["AllMakes"] = new SelectList(_vehicleRepo.GetAllVehicleMakes(), "VehicleMakeId", "VehicleMakeName");

            //new SelectList(listOfAllMakes, "VehicleMakeId", "VehicleMakeName");

            //get just the model names
            //var allModels = allVehicles.Select(v => v.VehicleModel).Distinct().ToList();

            //List<SelectListItem> listOfAllModels = allModels.Select(eachModel => new SelectListItem { Text = eachModel, Value = eachModel }).ToList();

            ViewData["AllModels"] = new SelectList(_vehicleRepo.GetAllVehicleModels(),"VehicleModelId", "VehicleModelName");

        }


        [HttpGet]//to get the view from the server to the browser (user) 
        public IActionResult SearchVehicles()
        {
            //DropDownList
            CreateDropDownLists();
            SearchVehiclesViewModel vm = new SearchVehiclesViewModel();
            //vm.SearchResult = new List<Vehicle>();
            return View(vm);
        }



        [HttpPost]//to send user data / actions to the server
        public IActionResult SearchVehicles(SearchVehiclesViewModel viewModel)
             //string vehicleStatus)
             //(int? searchMileage = null, DateTime? searchPurchaseDate = null, DateTime? searchServicedDate = null)   //parameters 
        {
            //DropDownList
            CreateDropDownLists();
            IEnumerable<Vehicle> allVehicles = _vehicleRepo.GetAllVehicles();

            //a = a + b

            if (viewModel.SearchMileage != null)
            {
                allVehicles = allVehicles.Where(a => a.Mileage >= viewModel.SearchMileage);
            }

            if (viewModel.SearchPurchaseDate != null)
            {
                //LINQ using Lambda expression
                allVehicles = allVehicles.Where(a => a.DatePurchased <= viewModel.SearchPurchaseDate);
            }

            if (viewModel.SearchServicedDate != null)
            {
                //put condition here
                allVehicles = allVehicles.Where(a => a.VehicleServiceRequests.Any(vsr => vsr.DateServiced >= viewModel.SearchServicedDate));
            }

            //Where is better than FindAll
            if(viewModel.SearchVehicleMakeId.HasValue)
            {
                allVehicles = allVehicles.Where(v => v.VehicleMakeId == viewModel.SearchVehicleMakeId);    
            }

            if (viewModel.SearchVehicleModelId.HasValue)
            {
                allVehicles = allVehicles.Where(v => v.VehicleModelId == viewModel.SearchVehicleModelId).ToList();
            }
            if (viewModel.VehicleStatus.HasValue)
            {
                allVehicles = allVehicles.Where(v => v.VehicleStatus == viewModel.VehicleStatus);
            }



            viewModel.SearchResult = allVehicles.ToList();

            return View(viewModel);
        }


    }
}
