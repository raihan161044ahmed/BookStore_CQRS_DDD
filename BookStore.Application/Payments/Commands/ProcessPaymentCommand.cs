using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Application.Interfaces;

namespace BookStore.Application.Payments.Commands
{
    public record ProcessPaymentCommand(Guid OrderId, decimal Amount, string Method) : IRequest<Guid>;

    public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, Guid>
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly IOrderRepository _orderRepo;

        public ProcessPaymentHandler(IPaymentRepository paymentRepo, IOrderRepository orderRepo)
        {
            _paymentRepo = paymentRepo;
            _orderRepo = orderRepo;
        }

        public async Task<Guid> Handle(ProcessPaymentCommand request, CancellationToken ct)
        {
            var order = await _orderRepo.GetByIdAsync(request.OrderId)
                        ?? throw new Exception("Order not found");

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Amount = request.Amount,
                Method = request.Method
            };

            await _paymentRepo.CreateAsync(payment);
            order.Status = "Paid";
            await _orderRepo.UpdateAsync(order);

            return payment.Id;
        }
    }
}
