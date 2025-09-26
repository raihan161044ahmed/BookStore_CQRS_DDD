using System;
using System.Collections.Generic;


namespace BookStore.Domain.Entities;


public sealed class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public string? ISBN { get; private set; }
    public decimal Price { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    public Guid? AuthorId { get; private set; }
    public Author? Author { get; private set; }
    public int Stock { get; private set; }


    // EF Core requires a parameterless constructor
    private Book() { }


    public Book(Guid id, string title, decimal price, int stock, Guid? authorId = null, string? isbn = null, string? description = null, DateTime? publishedAt = null)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Title = !string.IsNullOrWhiteSpace(title) ? title : throw new ArgumentException("Title is required", nameof(title));
        Price = price >= 0 ? price : throw new ArgumentOutOfRangeException(nameof(price));
        Stock = stock >= 0 ? stock : throw new ArgumentOutOfRangeException(nameof(stock));
        AuthorId = authorId;
        ISBN = isbn;
        Description = description;
        PublishedAt = publishedAt;
    }


    public void Update(string title, decimal price, int stock, string? description = null, string? isbn = null, DateTime? publishedAt = null)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required", nameof(title));
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));
        if (stock < 0) throw new ArgumentOutOfRangeException(nameof(stock));


        Title = title;
        Price = price;
        Stock = stock;
        Description = description;
        ISBN = isbn;
        PublishedAt = publishedAt;
    }


    public void IncreaseStock(int amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Stock += amount;
    }


    public void DecreaseStock(int amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        if (Stock - amount < 0) throw new InvalidOperationException("Insufficient stock");
        Stock -= amount;
    }
}