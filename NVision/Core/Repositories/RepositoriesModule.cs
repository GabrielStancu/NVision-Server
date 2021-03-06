using Microsoft.Extensions.DependencyInjection;

namespace Core.Repositories
{
    public static class RepositoriesModule
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<ISensorMeasurementRepository, SensorMeasurementRepository>();
            services.AddScoped<IWatcherRepository, WatcherRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IAlertRepository, AlertRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
        }
    }
}
