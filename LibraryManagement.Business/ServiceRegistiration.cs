using LibraryManagement.Business.Services;
using LibraryManagement.Domain.Abstractions.Storage;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Business
{
    // Provides extension methods to register business services in the IServiceCollection.
    public static class ServiceRegistration
    {
        // Adds the necessary business services to the IServiceCollection.
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly));
            services.AddHttpClient();
            services.AddScoped<IStorageService, StorageService>();
        }
    }
}
