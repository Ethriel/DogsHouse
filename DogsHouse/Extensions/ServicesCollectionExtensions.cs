using AspNetCoreRateLimit;
using DogsHouse.Database;
using DogsHouse.Database.Model;
using DogsHouse.Services;
using DogsHouse.Services.Validation;
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
                           .AddScoped<IValidator<Dog>, DogValidator>();
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
            var endpoint = "*";
            var limitPerSecond = 10;
            var limitPerMinute = limitPerSecond * 60;
            var limitPerHour = limitPerMinute * 60;
            var limitPerDay = limitPerHour * 24;

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
                               Endpoint = endpoint,
                               Period = "1s",
                               Limit = limitPerSecond
                           },
                           new RateLimitRule
                           {
                               Endpoint = endpoint,
                               Period = "1m",
                               Limit = limitPerMinute
                           },
                           new RateLimitRule
                           {
                               Endpoint = endpoint,
                               Period = "1h",
                               Limit = limitPerHour
                           },
                           new RateLimitRule
                           {
                               Endpoint = endpoint,
                               Period = "1d",
                               Limit = limitPerDay
                           }
                       };
            });
        }
    }
}
