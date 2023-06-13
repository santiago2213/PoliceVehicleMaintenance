using Microsoft.AspNetCore.Identity.UI.Services;

namespace DiscussionMvcSantiago.Services
{
    public class ServiceRequestEmailSender : IServiceRequestEmailSender
    {
        private IEmailSender _emailSender;
        private IHttpContextAccessor _contextAccessor;

        public ServiceRequestEmailSender(IEmailSender emailSender, 
            IHttpContextAccessor contextAccessor)
        {
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
        }

        public void SendServiceRequestEmail(string controllerAndMethodNames, string email, string subject, string message)
        {
            string callbackUrl = @"https://" + _contextAccessor.HttpContext.Request.Host.Value + "/" + controllerAndMethodNames;

            _emailSender.SendEmailAsync(email, subject, message + $"by clicking <a href='{callbackUrl}'>here</a>").Wait();
        }
    }
}
