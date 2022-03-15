using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Helpers
{
    public static class HelpersModule
    {
        public static void AddHelpers(this IServiceCollection services)
        {
            services.AddScoped<IProfilePictureUrlResolver, ProfilePictureUrlResolver>();
            services.AddScoped<IReadingToMeasurementConverter, ReadingToMeasurementConverter>();
            services.AddScoped<ISensorNameMapper, SensorNameMapper>();
        }
    }
}
