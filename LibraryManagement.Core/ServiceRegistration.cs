using System.Reflection;
using FluentValidation;
using LibraryManagement.Core.Middleware;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Core
{
    // Represents a static class for registering core services in the application.
    public static class ServiceRegistration
    {
        // Adds the core services to the specified IServiceCollection.
        public static void AddCoreServices(this IServiceCollection services)
        {
            // Registers validators from the current executing assembly.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Registers the RequestValidationBehavior as a transient service.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }
    }
}
