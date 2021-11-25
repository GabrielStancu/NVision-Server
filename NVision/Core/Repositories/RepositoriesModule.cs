using Microsoft.Extensions.DependencyInjection;

namespace Core.Repositories
{
    public static class RepositoriesModule
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped(typeof(IUserRepository<>), (typeof(UserRepository<>)));
            services.AddScoped<ISensorMeasurementRepository, SensorMeasurementRepository>();
            services.AddScoped<IWatcherRepository, WatcherRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
        }
    }
}
