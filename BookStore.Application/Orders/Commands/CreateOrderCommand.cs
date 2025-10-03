using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands
{
    public record CreateOrderCommand(Guid UserId, List<OrderItemDto> Items) : IRequest<Guid>;

    public record OrderItemDto(Guid BookId, int Quantity, decimal Price);
}
