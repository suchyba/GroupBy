using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(string recipentEmail, string subject, string body);
    }
}
