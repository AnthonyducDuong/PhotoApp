using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Interfaces.IServices
{
    public interface IMailService
    {
        public Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
