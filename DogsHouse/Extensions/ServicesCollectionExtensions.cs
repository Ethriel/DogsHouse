using AspNetCoreRateLimit;
using DogsHouse.Database;
using DogsHouse.Services;
using DogsHouse.Services.Abstraction;
using DogsHouse.Services.DataPresentation;
using DogsHouse.Services.Model;
using DogsHouse.Services.Validation;
using DogsHouse.Utility;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            return services.AddScoped<DbContext, DogsHouseContext>()
                           .AddScoped(typeof(IEntityService<>), typeof(EntityService<>))
                           .AddScoped(typeof(IExtendedEntityService<>), typeof(ExtendedEntityService<>))
                           .AddScoped(typeof(IMapperService<,>), typeof(MapperService<,>))
                           .AddScoped<IValidator<DogDTO>, DogValidator>()
                           .AddScoped<IValidator<DogsSortingFilter>, DogsSortingValidator>()
                           .AddScoped<IValidator<DogsPagination>, DogsPagingValidator>()
                           .AddScoped<IDogService, DogService>();
        }

        public static IServiceCollection AddRateLimiting(this IServiceCollection services)
        {


            return services.AddMemoryCache()
                           .ConfigureRateLimiting()
                           .AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>()
                           .AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>()
                           .AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>()
                           .AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>()
                           .AddInMemoryRateLimiting();
        }

        public static IServiceCollection ConfigureRateLimiting(this IServiceCollection services)
        {
            return services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.RealIpHeader = "X-Real-IP";
                options.ClientIdHeader = "X-ClientId";
                options.GeneralRules = new List<RateLimitRule>
                       {
                           new RateLimitRule
                           {
                               Endpoint = "*",
                               Period = "1s",
                               Limit = 10
                           }
                       };
            });
        }

        public static IServiceCollection AddAppOptions(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddOptions()
                           .Configure<ApplicationOptions>(configuration.GetSection("ApplicationOptions"));
        }
    }
}
