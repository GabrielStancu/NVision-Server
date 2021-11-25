using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Convertors
{
    public static class ConvertorsModule
    {
        public static void AddConvertors(this IServiceCollection services)
        {
            services.AddScoped<ISensorTypeToSensorMeasurementConvertor, SensorTypeToSensorMeasurementConvertor>();
        }
    }
}
