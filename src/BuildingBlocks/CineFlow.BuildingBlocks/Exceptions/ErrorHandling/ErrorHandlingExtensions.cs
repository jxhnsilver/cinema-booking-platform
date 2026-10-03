using Microsoft.Extensions.DependencyInjection;

namespace CineFlow.BuildingBlocks.Exceptions.ErrorHandling;

public static class ErrorHandlingExtensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
}
