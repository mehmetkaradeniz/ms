using Microsoft.Extensions.Logging;
using Order.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Infrastructure.Persistence
{
    public class OrderContextSeed
    {

        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetData());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Seed success {typeof(OrderContext).Name}");
            }
        }

        private static List<OrderEntity> GetData()
        {
            return new List<OrderEntity>()
            {
                new OrderEntity(){ Username = "ms", FirstName = "mehmet", LastName = "karadeniz", EmailAddress = "mehmetkz61@gmail.com", AddressLine = "AddressLine", TotalPrice = 350}
            };
        }
    }
}
