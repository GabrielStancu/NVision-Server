using Infrastructure.Filtering.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Filtering
{
    public static class FilteringModule
    {
        public static void AddFiltering(this IServiceCollection services)
        {
            AddFilters(services);
        }

        private static void AddFilters(IServiceCollection services)
        {
            services.AddScoped<IAlertFilter, AlertFilter>();
            services.AddScoped<ISensorMeasurementFilter, SensorMeasurementFilter>();
            services.AddScoped<ISubjectFilter, SubjectFilter>();
        }
    }
}
