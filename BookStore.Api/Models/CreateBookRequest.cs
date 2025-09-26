namespace BookStore.Api.Models
{
   public record CreateBookRequest(string Title, decimal Price, int Stock, Guid? AuthorId, string? ISBN, string? Description, DateTime? PublishedAt);
}
