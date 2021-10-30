using Discount.Grpc.Protos;
using System;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService : IDiscountService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _grpcClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient grpcClient)
        {
            _grpcClient = grpcClient ?? throw new ArgumentNullException(nameof(grpcClient));
        }


        public async Task<CouponModel> Get(string productName)
        {
            var request = new GetRequest { ProductName = productName };
            return await _grpcClient.GetAsync(request);
        }
    }
}
