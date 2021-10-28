using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<BasketEntity> Get(string username);
        Task<BasketEntity> Update(BasketEntity basket);
        Task Delete(string username);
    }
}