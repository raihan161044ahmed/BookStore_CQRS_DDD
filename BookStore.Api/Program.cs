using BookStore.Infrastructure.Extensions;
using Mapster;
using MapsterMapper;
using FluentValidation;



var builder = WebApplication.CreateBuilder(args);


// Add Application + Infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);


// Add MediatR + Validators
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BookStore.Application.Books.Commands.CreateBookCommand).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(BookStore.Application.Books.Commands.CreateBookCommandValidator).Assembly);


// Add Mapster
var config = TypeAdapterConfig.GlobalSettings;
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();


// Add Controllers
builder.Services.AddControllers();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


app.Run();