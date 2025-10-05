using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BookStore.Infrastructure.Repositories
{
    public class PaymentRepository(BookStoreDbContext db) : IPaymentRepository
    {
        private readonly BookStoreDbContext _db = db;

        public async Task<IEnumerable<Payment>> GetAllAsync() =>
            await _db.Payments.ToListAsync();

        public async Task<Payment?> GetByIdAsync(Guid id) =>
            await _db.Payments.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Payment>> GetByOrderIdAsync(Guid orderId) =>
            await _db.Payments.Where(p => p.OrderId == orderId).ToListAsync();

        public async Task<Guid> CreateAsync(Payment payment)
        {
            _db.Payments.Add(payment);
            await _db.SaveChangesAsync();
            return payment.Id;
        }

        public async Task UpdateAsync(Payment payment)
        {
            _db.Payments.Update(payment);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var payment = await _db.Payments.FindAsync(id);
            if (payment is null) return;
            _db.Payments.Remove(payment);
            await _db.SaveChangesAsync();
        }
    }
}
