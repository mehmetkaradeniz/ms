using System.Collections.Generic;

namespace Shopping.Aggregator.Models
{
    public class ShoppingModel
    {
        public string Username { get; set; }
        public BasketModel BasketWithProducts { get; set; }
        public ICollection<OrderResponseModel> Orders { get; set; }
    }
}
