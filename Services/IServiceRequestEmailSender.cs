namespace DiscussionMvcSantiago.Services
{
    public interface IServiceRequestEmailSender
    {
        public void SendServiceRequestEmail(string controllerAndMethodNames,string email, string subject, string message);


    }
}
