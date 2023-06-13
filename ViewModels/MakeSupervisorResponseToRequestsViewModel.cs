

using DiscussionLibrarySantiago;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace DiscussionMvcSantiago.ViewModels
{
    public class MakeSupervisorResponseToRequestsViewModel
    {
        [Required(ErrorMessage = "Service Request ID is required")]
        public int ServiceRequestId { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public Notes SupervisorResponseNotes { get; set; }
    }
}
