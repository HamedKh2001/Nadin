using Microsoft.Extensions.DependencyInjection;
using Nadin.Infrastucture.Persistence;
using System;

namespace Nadin.Infrastucture.Middlewares
{
    public static class InfraMiddlewareExtension
    {
        public static void DbContextInitializer(this IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<NadinDbContextInitializer>();
                initialiser.Initialize();
                initialiser.Seed();
            }
        }
    }
}
