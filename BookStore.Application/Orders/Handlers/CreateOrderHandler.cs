using BookStore.Application.Orders.Commands;
using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Application.Interfaces;

namespace BookStore.Application.Orders.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepo;
        public CreateOrderHandler(IOrderRepository orderRepo) => _orderRepo = orderRepo;

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken ct)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Items = request.Items.Select(i => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    BookId = i.BookId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            await _orderRepo.CreateAsync(order);
            return order.Id;
        }
    }
}
