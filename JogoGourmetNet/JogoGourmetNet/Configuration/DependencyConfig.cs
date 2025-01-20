using JogoGourmetNet.Presentation;
using JogoGourmetNet.Repository;
using JogoGourmetNet.Service.External;
using JogoGourmetNet.Service.Internal;
using JogoGourmetNet.Utils.RandomUtil;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace JogoGourmetNet.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class DependencyConfig
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddSingleton<ILogicGame,   LogicGame>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IUserInterface,  ConsoleInterface>();
            services.AddScoped<IRandomUtil,     RandomUtil>();
            services.AddScoped<IGameService,    GameService>();

            return services;
        }
    }
}