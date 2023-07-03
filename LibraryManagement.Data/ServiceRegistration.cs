using LibraryManagement.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Data
{
    public static class ServiceRegistration
    {
        /// <summary>
        /// Registers persistence services in the dependency injection container.
        /// </summary>
        public static void AddPersestenceServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LibraryManagementDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }
    }
}
