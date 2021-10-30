using Discount.Grpc.Protos;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public interface IDiscountService
    {
        Task<CouponModel> Get(string productName);
    }
}