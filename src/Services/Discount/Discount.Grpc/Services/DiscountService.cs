using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository repository, IMapper mapper, ILogger<DiscountService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<CouponModel> Get(GetRequest request, ServerCallContext context)
        {
            var coupon = await _repository.Get(request.ProductName);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));

            _logger.LogInformation($"Discount is retrieved for ProductName : {coupon.ProductName}, Amount: {coupon.Amount}");
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> Create(CreateRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<CouponEntity>(request.Coupon);
            await _repository.Create(coupon);
            _logger.LogInformation($"Discount is successfully created. Product {coupon.ProductName}");

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> Update(UpdateRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<CouponEntity>(request.Coupon);
            await _repository.Update(coupon);
            _logger.LogInformation($"Discount is successfully updated. Product {coupon.ProductName}");

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
        {
            var deleted = await _repository.Delete(request.ProductName);
            var response = new DeleteResponse
            {
                Success = deleted
            };

            return response;
        }
    }
}
