using AspnetRunBasics.Models;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public interface IBasketService
    {
        Task<BasketModel> GetByUsername(string username);
        Task<BasketModel> Update(BasketModel model);
        Task Checkout(BasketCheckoutModel model);
    }
}
