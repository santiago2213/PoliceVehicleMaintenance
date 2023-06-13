using DiscussionLibrarySantiago;
using DiscussionMvcSantiago.Models;
using DiscussionMvcSantiago.Services;
using DiscussionMvcSantiago.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using static DiscussionLibrarySantiago.ServiceRequest;

namespace DiscussionMvcSantiago.Controllers
{
    public class ServiceRequestController : Controller
    {
        private IServiceRequestRepo _serviceRequestRepo;
        private IAppUserRepo _appUserRepo;
        private IVehicleRepo _vehicleRepo;
        private IServiceRequestEmailSender _serviceRequestEmailSender;

        public ServiceRequestController(IServiceRequestRepo serviceRequestRepo, IAppUserRepo appUserRepo, IVehicleRepo vehicleRepo, IServiceRequestEmailSender serviceRequestEmailSender)
        {
            _serviceRequestRepo = serviceRequestRepo;
            _appUserRepo = appUserRepo;
            _vehicleRepo = vehicleRepo;
            _serviceRequestEmailSender = serviceRequestEmailSender;
        }

        public void CreateDropDownList()
        {
            List<Vehicle> allVehicles = _vehicleRepo.GetAllVehicles().ToList();
            var allVehiclesForDDL = allVehicles.Select(a => new
            {
                VehicleId = a.VehicleId,
                VehicleDetails = a.VIN + " - " + a.VehicleMake.VehicleMakeName +
                " - " + a.VehicleModel.VehicleModelName + " - " + a.Year
            });

            ViewData["AllVehicles"] = new SelectList(allVehiclesForDDL, "VehicleId", "VehicleDetails");
            ViewData["AllMechanics"] = new SelectList(_appUserRepo.GetAllMechanics(), "Id", "Fullname");
        }

        [HttpPost]
        public IActionResult MakeSupervisorResponseToRequests(List<string> ServiceRequestIds, List<string> Responses, List<string> SupervisorResponses)
        {
            string supervisorId = _appUserRepo.GetUserId();
            int numberOfResponses = ServiceRequestIds.Count;

            for (int i = 0; i < numberOfResponses; i++)
            {
                int serviceRequestId = Convert.ToInt32(ServiceRequestIds[i]);
                ServiceRequest serviceRequest = _serviceRequestRepo.GetServiceRequest(serviceRequestId);

                VehicleStatusOptions supervisorResponse = (VehicleStatusOptions)Convert.ToInt32(Responses[i]);

                if (supervisorResponse == VehicleStatusOptions.BeingServiced)
                {
                    serviceRequest.SupervisorDecision = SupervisorDecisionOptions.inservice;

                    Vehicle vehicle = _vehicleRepo.FindVehicle(serviceRequest.VehicleId);
                    vehicle.VehicleStatus = VehicleStatusOptions.BeingServiced;
                    _vehicleRepo.UpdateVehicle(vehicle);
                }

                if (supervisorResponse == VehicleStatusOptions.Retired)
                {
                    serviceRequest.SupervisorDecision = SupervisorDecisionOptions.denied;

                    Vehicle vehicle = _vehicleRepo.FindVehicle(serviceRequest.VehicleId);
                    vehicle.VehicleStatus = VehicleStatusOptions.Retired;
                    _vehicleRepo.UpdateVehicle(vehicle);
                }

                if (SupervisorResponses[i] != null)
                {
                    Notes note = new Notes(SupervisorResponses[i], serviceRequestId, supervisorId, null);
                    _serviceRequestRepo.AddNote(note);
                }

                _serviceRequestRepo.UpdateServiceRequest(serviceRequest);

            }

            return RedirectToAction("SearchServiceRequests");
        }

        [HttpGet]
        public IActionResult MakeServiceRequest()
        {
            CreateDropDownList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="Officer")]
        public IActionResult MakeServiceRequest(MakeServiceRequestViewModel viewModel) 
        { 
            string officerId = _appUserRepo.GetUserId();

            if (_serviceRequestRepo.HasRequestBeenMade(viewModel.VehicleId))
            {
                ModelState.AddModelError("RepeatServiceRequestError","A service request was already mad today for this vehicle");
            }

                if (ModelState.IsValid)
                {
                    ServiceRequest serviceRequest = new ServiceRequest(
                        viewModel.Description,
                        viewModel.VehicleId, 
                        officerId);

                    SupervisorOfficer supervisorOfficer = _appUserRepo.GetOfficerSupervisors(officerId);

                    serviceRequest.SupervisorId = supervisorOfficer.SupervisorId;

                    int serviceRequestId = this._serviceRequestRepo.MakeServiceRequest(serviceRequest);

                    Supervisor supervisor = _appUserRepo.GetSupervisor(supervisorOfficer.SupervisorId);

                    string subject = "New service request to be decided";
                    string message = $"View your service request";

                    _serviceRequestEmailSender.SendServiceRequestEmail("ServiceRequest/GetAllUndecidedRequestsForSupervisor", supervisor.Email, subject, message);

                    //string callbackUrl =
                    //    Url.Action("GetAllUndecidedRequestsForSupervisor", "ServiceRequest", new {ServiceRequestId = serviceRequestId}, Request.Scheme, Request.Host.Value);

                    //Supervisor supervisor = _appUserRepo.GetSupervisor(supervisorOfficer.SupervisorId);

                    //string subject = "New service request to be decided";
                    //string message = $"View your new service request by clicking <a href='{callbackUrl}'>here</a>";

                    //_emailSender.SendEmailAsync(supervisor.Email, subject, message).Wait();

                    return RedirectToAction("SearchServiceRequests");
                }
            else //when there are errors
            {
                CreateDropDownList();
                return View(viewModel);
            }
        }

        [Authorize(Roles = "Mechanic")]
        public IActionResult GetAllUnservicedRequestsForMechanic()
        {
            CreateDropDownList();
            string mechanicId = _appUserRepo.GetUserId();
            List<ServiceRequest> unservicedServiceRequests =
                _serviceRequestRepo.GetAllUnservicedRequestsForMechanic(mechanicId);

            return View(unservicedServiceRequests);
        }

        public IActionResult MakeMechanicDecisionsOnRequests(List<string> ServiceRequestIds, List<string> Decisions, List<string> MechanicNotes)
        {
            string mechanicId = _appUserRepo.GetUserId();

            int numberOfDecisions = ServiceRequestIds.Count;

            for (int i = 0; i < numberOfDecisions; i++)
            {
                int serviceRequestId = Convert.ToInt32(ServiceRequestIds[i]);
                ServiceRequest serviceRequest = _serviceRequestRepo.GetServiceRequest(serviceRequestId);

                    VehicleStatusOptions mechanicDecision = (VehicleStatusOptions)Convert.ToInt32(Decisions[i]);

                if(mechanicDecision == VehicleStatusOptions.Running)
                {
                    serviceRequest.SupervisorDecision = SupervisorDecisionOptions.complete;
                    serviceRequest.DateServiced= DateTime.Now;

                    Vehicle vehicle = _vehicleRepo.FindVehicle(serviceRequest.VehicleId);
                    vehicle.VehicleStatus = VehicleStatusOptions.Running;
                    _vehicleRepo.UpdateVehicle(vehicle);
                }
                if (MechanicNotes[i] != null)
                {
                    Notes note = new Notes(MechanicNotes[i], serviceRequestId, mechanicId, null);
                    //serviceRequest.ServiceRequestNotes.Add(note);
                    _serviceRequestRepo.AddNote(note);
                }

                _serviceRequestRepo.UpdateServiceRequest(serviceRequest);

                //string mechanicId = AssignedMechanicIds[i];
                //MakeDecisionOnOneRequest(serviceRequestId, supervisorDecision, mechanicId);

            }

            return RedirectToAction("SearchServiceRequests");
        }

        [Authorize(Roles ="Supervisor")]
        public IActionResult GetAllUndecidedRequestsForSupervisor()
        {
            CreateDropDownList();
            string supervisorId = _appUserRepo.GetUserId();
            List<ServiceRequest> undecidedServiceRequests = 
                _serviceRequestRepo.GetAllUndecidedRequestsForSupervisor(supervisorId);

            return View(undecidedServiceRequests);
        }

        [HttpPost]
        public IActionResult MakeDecisionOnRequests(List<string> ServiceRequestIds, List<string> Decisions, List<string> AssignedMechanicIds)
        {
            int numberOfDecisions = ServiceRequestIds.Count;
            
            for(int i = 0; i < numberOfDecisions; i++)
            {
                if (Decisions[i] != null)
                {
                    SupervisorDecisionOptions supervisorDecision = (SupervisorDecisionOptions)Convert.ToInt32(Decisions[i]);
                    int serviceRequestId = Convert.ToInt32(ServiceRequestIds[i]);
                    string mechanicId = AssignedMechanicIds[i];
                    MakeDecisionOnOneRequest(serviceRequestId, supervisorDecision, mechanicId);
                }
            }

            return RedirectToAction("SearchServiceRequests");
        }

        public void MakeDecisionOnOneRequest(int serviceRequestId, SupervisorDecisionOptions decision, string mechanicId)
        {
            ServiceRequest serviceRequest = _serviceRequestRepo.GetServiceRequest(serviceRequestId);
            serviceRequest.SupervisorDecision = decision;
            serviceRequest.DateSupervisorDecision = DateTime.Now;
            if(decision == SupervisorDecisionOptions.approved)
            {
                serviceRequest.MechanicId = mechanicId;
                serviceRequest.SupervisorDecision = SupervisorDecisionOptions.inservice;
                Vehicle vehicle = _vehicleRepo.FindVehicle(serviceRequest.VehicleId);
                vehicle.VehicleStatus = VehicleStatusOptions.BeingServiced;
                _vehicleRepo.UpdateVehicle(vehicle);
            }
            _serviceRequestRepo.UpdateServiceRequest(serviceRequest);

            Officer officer = _appUserRepo.GetOfficer(serviceRequest.OfficerId);

            string subject = "Decision on your service request";
            string message = "View your service request decision";
            _serviceRequestEmailSender.SendServiceRequestEmail
                ("ServiceRequest/SearchServiceRequests?serviceRequestId=" + serviceRequestId, officer.Email, subject, message);
        }

        [HttpGet]
        public IActionResult SearchServiceRequests()
        {
            CreateDropDownList();
            SearchServiceRequestsViewModel viewModel = new SearchServiceRequestsViewModel();
            return View(viewModel); 
        }

        [HttpPost]
        public IActionResult SearchServiceRequests(SearchServiceRequestsViewModel viewModel)
        {
            CreateDropDownList();

            List<ServiceRequest> serviceRequests = _serviceRequestRepo.GetAllServiceRequests();

            if (viewModel.SearchVIN != null)
            {
                serviceRequests = serviceRequests.Where(sr => sr.Vehicle.VIN == viewModel.SearchVIN).ToList();
            }
            //if (viewModel.SearchVehicleId!= null) 
            //{ 
            //    serviceRequests = serviceRequests.Where(sr => sr.VehicleId == viewModel.SearchVehicleId).ToList();
            //}
            //if (viewModel.SearchDateServiceRequested != null)
            //{
            //    serviceRequests = serviceRequests.Where(sr => sr.DateServiceRequested <= viewModel.SearchDateServiceRequested).ToList();
            //}
            if (viewModel.SearchMechanicFullName != null)
            {
                serviceRequests = serviceRequests.Where(sr => sr.ServiceRequestNotes.Any(n => n.Mechanic.Fullname.Contains(viewModel.SearchMechanicFullName))).ToList();
            }
            //if (viewModel.SearchOfficer != null)
            //{
            //    serviceRequests = serviceRequests.Where(sr => sr.Officer.Id == viewModel.SearchOfficer).ToList();
            //}

            viewModel.Result = serviceRequests;

            return View(viewModel);
        }

        //public void CreateDropDownLists()
        //{
        //    ViewData["AllVins"] = new SelectList(_vehicleRepo.GetAllVehicles(), "VehicleId", "VIN");
        //    ViewData["AllMechanics"] = new SelectList(_appUserRepo.GetAllMechanics(), "Id", "Fullname");
        //}

    }
}

//string description, string vin, string officerName, DateTime dateServiceRequested, string supervisorName, string? mechanicName = null, DateTime? dateDecided = null, string? mechanicNotes = null, DateTime? dateServiced = null, string? supervisorNotes = null, DateTime? dateCompleted = null