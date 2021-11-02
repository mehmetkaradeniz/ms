using AutoMapper;
using Order.Application.Features.Orders.Commands.CheckoutOrder;
using Order.Application.Features.Orders.Commands.UpdateOrder;
using Order.Application.Features.Orders.Queries.GetOrdersList;
using Order.Domain.Entities;

namespace Order.Application.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<OrderEntity, OrdersVm>().ReverseMap();
            CreateMap<OrderEntity, CheckoutOrderCommand>().ReverseMap();
            CreateMap<OrderEntity, UpdateOrderCommand>().ReverseMap();
        }
    }
}
