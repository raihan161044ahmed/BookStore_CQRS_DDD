namespace BookStore.Api.Models
{
  public record BookDto(Guid Id, string Title, decimal Price, int Stock, string? Description, string? ISBN, DateTime? PublishedAt, string? AuthorName);
}
