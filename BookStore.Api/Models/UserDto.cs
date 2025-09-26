namespace BookStore.Api.Models
{
    public record UserDto(Guid Id, string Email, string? Role);
}
