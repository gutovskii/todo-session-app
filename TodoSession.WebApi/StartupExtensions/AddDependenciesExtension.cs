using Microsoft.Extensions.DependencyInjection;
using TodoSession.BLL.Services.Interfaces;
using TodoSession.BLL.Services.Realization;

namespace TodoSession.WebApi.StartupExtensions
{
    public static class AddDependenciesExtension
    {
        public static IServiceCollection AddBL(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITodoService, TodoService>();
            return services;
        }
    }
}
