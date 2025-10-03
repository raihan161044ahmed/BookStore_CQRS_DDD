using BookStore.Application.Payments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(ProcessPaymentCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { PaymentId = id });
        }
    }
}
