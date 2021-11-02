using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Contracts.Persistence;
using Order.Application.Exceptions;
using Order.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);
            if (orderToDelete == null)
                throw new NotFoundException(nameof(OrderEntity), request.Id);

            await _orderRepository.DeleteAsync(orderToDelete);

            return Unit.Value;
        }
    }
}
