using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Persistence;
using System;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class OrderRepository(BookStoreDbContext db) : IOrderRepository
    {
        private readonly BookStoreDbContext _db = db;

        public async Task<IEnumerable<Order>> GetAllAsync() =>
            await _db.Orders.Include(o => o.Items).ToListAsync();

        public async Task<Order?> GetByIdAsync(Guid id) =>
            await _db.Orders.Include(o => o.Items)
                            .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<Guid> CreateAsync(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return order.Id;
        }

        public async Task UpdateAsync(Order order)
        {
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order is null) return;
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
        }
    }
}
