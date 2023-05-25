using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace MyBase.Application.Infrastructure
{
    public static class MediatorModule
    {
        public static IServiceCollection AddProductModule(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(MediatorModule).Assembly);
            return serviceCollection;
        }
    }
}
