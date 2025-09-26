using BookStore.Domain.Entities;
using BookStore.Api.Models;
using Mapster;


namespace BookStore.Api.Mappings;


public static class MappingConfig
{
    public static void RegisterMappings(TypeAdapterConfig config)
    {
        config.NewConfig<Book, BookDto>();
        config.NewConfig<CreateBookRequest, BookStore.Application.Books.Commands.CreateBookCommand>();


        config.NewConfig<User, UserDto>();
    }
}