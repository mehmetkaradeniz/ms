using Order.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<OrderEntity>
    {
        Task<IReadOnlyList<OrderEntity>> GetByUsername(string username);
    }
}
