using AspnetRunBasics.Models;
using AspnetRunBasics.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetByUsername(string username);
    }
}
