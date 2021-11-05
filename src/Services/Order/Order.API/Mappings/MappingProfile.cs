using AutoMapper;
using EventBus.Messages.Events;
using Order.Application.Features.Orders.Commands.CheckoutOrder;

namespace Order.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
