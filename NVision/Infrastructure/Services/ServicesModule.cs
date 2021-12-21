using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    public static class ServicesModule
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDataGeneratorService, DataGeneratorService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IAlertingService, AlertingService>();
            services.AddScoped<ISubjectDataService, SubjectDataService>();
            services.AddScoped<IWatcherDataService, WatcherDataService>();
        }
    }
}
