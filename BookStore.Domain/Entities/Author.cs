using System;
using System.Collections.Generic;


namespace BookStore.Domain.Entities;


public sealed class Author
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Bio { get; private set; }


    // Navigation
    public ICollection<Book> Books { get; private set; } = new List<Book>();


    private Author() { }


    public Author(Guid id, string name, string? bio = null)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name is required", nameof(name));
        Bio = bio;
    }


    public void Update(string name, string? bio = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
        Name = name;
        Bio = bio;
    }
}