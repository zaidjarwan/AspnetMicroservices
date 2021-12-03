using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository,
                                         IMapper mapper,
                                         ILogger<DeleteOrderCommandHandler> logger)
        {
            this._orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToBeDeleted = await _orderRepository.GetByIdAsync(request.Id);
            if (orderToBeDeleted == null)
            {
                _logger.LogError("Order not exist in database.");
            }

            await _orderRepository.DeleteAsync(orderToBeDeleted);
            _logger.LogInformation($"Order {orderToBeDeleted.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
