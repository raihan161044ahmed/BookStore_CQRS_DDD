using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities;
using BookStore.Application.Interfaces;
using BookStore.Infrastructure.Persistence;


namespace BookStore.Infrastructure.Repositories;


public class BookRepository : IBookRepository
{
    private readonly BookStoreDbContext _db;


    public BookRepository(BookStoreDbContext db)
    {
        _db = db;
    }


    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id, ct);
    }


    public async Task AddAsync(Book book, CancellationToken ct = default)
    {
        await _db.Books.AddAsync(book, ct);
    }


    public void Update(Book book)
    {
        _db.Books.Update(book);
    }


    public void Remove(Book book)
    {
        _db.Books.Remove(book);
    }


    public async Task<IPagedResult<Book>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var query = _db.Books.Include(b => b.Author).AsNoTracking();
        var total = await query.CountAsync(ct);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return new PagedResult<Book>(items, total, page, pageSize);
    }
}