using Order.Application.Contracts.Persistence;
using Order.Domain.Entities;
using Order.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<OrderEntity>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<OrderEntity>> GetByUsername(string username)
        {
            return await GetAsync(i => i.Username == username);
        }
    }
}
