using Elements.APNG.Serverless.Models.Model;
using System.Threading.Tasks;

namespace Elements.APNG.Serverless.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailTemplateMessage message);
    }
}
