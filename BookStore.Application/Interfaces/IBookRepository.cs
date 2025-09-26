using BookStore.Domain.Entities;


namespace BookStore.Application.Interfaces;


public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Book book, CancellationToken ct = default);
    void Update(Book book);
    void Remove(Book book);
    Task<IPagedResult<Book>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);
}