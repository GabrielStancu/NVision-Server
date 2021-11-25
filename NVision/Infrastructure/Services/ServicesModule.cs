using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    public static class ServicesModule
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDataGeneratorService, DataGeneratorService>();
        }
    }
}
