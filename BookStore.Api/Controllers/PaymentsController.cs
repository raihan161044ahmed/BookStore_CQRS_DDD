using BookStore.Application.Interfaces;
using BookStore.Application.Payments.Commands;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class PaymentsController(IPaymentRepository repo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await repo.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var payment = await repo.GetByIdAsync(id);
            return payment is not null ? Ok(payment) : NotFound();
        }

        [HttpGet("order/{orderId:guid}")]
        public async Task<IActionResult> GetByOrder(Guid orderId) =>
            Ok(await repo.GetByOrderIdAsync(orderId));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Payment payment)
        {
            var id = await repo.CreateAsync(payment);
            return Ok(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Payment payment)
        {
            payment.Id = id;
            await repo.UpdateAsync(payment);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
