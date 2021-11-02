using Order.Application.Models;
using System.Threading.Tasks;

namespace Order.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendMail(Email email);
    }
}
