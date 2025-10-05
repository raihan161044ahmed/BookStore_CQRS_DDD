using BookStore.Application.Interfaces;
using BookStore.Application.Orders.Commands;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController(IOrderRepository repo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await repo.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await repo.GetByIdAsync(id);
            return order is not null ? Ok(order) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            var id = await repo.CreateAsync(order);
            return Ok(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Order order)
        {
            order.Id = id;
            await repo.UpdateAsync(order);
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
