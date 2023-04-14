namespace GroupBy.Design.Services
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(string recipentEmail, string subject, string body);
    }
}
