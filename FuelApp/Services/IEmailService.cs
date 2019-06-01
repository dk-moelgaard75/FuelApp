using System.Threading.Tasks;

namespace FuelApp.Services
{
    public interface IEmailService
    {
        Task SendEmail(string emailTo, string subject, string message);
    }
}