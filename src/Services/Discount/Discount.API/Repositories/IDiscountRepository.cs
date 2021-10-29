using Discount.API.Entities;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public interface IDiscountRepository
    {
        Task<CouponEntity> Get(string productName);

        Task<bool> Create(CouponEntity coupon);

        Task<bool> Update(CouponEntity coupon);

        Task<bool> Delete(string productName);
    }
}
