using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BM.Framework.Mongo
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoContext<TMongoContext>(
            this IServiceCollection services,
                Func<IServiceProvider, TMongoContext> contextFactory)
            where TMongoContext : MongoContext
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (contextFactory is null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            services.AddScoped(contextFactory);

            services.AddScoped<IMongoRepository>(x =>
            {
                var mongoContext = x.GetService<TMongoContext>();
                var logger = x.GetService<ILogger<MongoRepository>>();
                return new MongoRepository(logger, mongoContext);
            });

            return services;
        }

        public static IServiceCollection AddMongoPagedDataFactory(
            this IServiceCollection services)
        {
            return services.AddSingleton<IMongoPagedDataFactory, MongoPagedDataFactory>();
        }
    }
}
