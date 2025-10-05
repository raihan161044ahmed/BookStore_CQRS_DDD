using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Payment>> GetByOrderIdAsync(Guid orderId);
        Task<Guid> CreateAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task DeleteAsync(Guid id);
    }
}
