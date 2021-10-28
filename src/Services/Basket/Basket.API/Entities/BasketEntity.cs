using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class BasketEntity
    {

        public BasketEntity()
        {
        }

        public BasketEntity(string username)
        {
            Username = username;
        }

        public string Username { get; set; }

        public List<BasketItemEntity> Items { get; set; } = new List<BasketItemEntity>();

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                    totalPrice += item.Price * item.Quantity;

                return totalPrice;
            }
        }
    }
}
