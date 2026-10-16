using CineFlow.BuildingBlocks.Exceptions.ErrorHandling;
using CineFlow.Catalog.Api.Data;
using CineFlow.Catalog.Api.Features.Movies.Create;
using CineFlow.Catalog.Api.Features.Movies.Delete;
using CineFlow.Catalog.Api.Features.Movies.GetAll;
using CineFlow.Catalog.Api.Features.Movies.GetById;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<CreateMovieUseCase>();
builder.Services.AddScoped<GetMovieByIdUseCase>();
builder.Services.AddScoped<GetAllMoviesUseCase>();
builder.Services.AddScoped<DeleteMovieUseCase>();

builder.Services.AddExceptionHandling();

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
