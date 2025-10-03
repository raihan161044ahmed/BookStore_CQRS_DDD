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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BookStoreDbContext _db;

        public PaymentRepository(BookStoreDbContext db)
        {
            _db = db;
        }

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
    }
}
