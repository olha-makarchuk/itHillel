using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Service;

namespace OnlineStore.Api.Modules
{
    public static class CoreModule
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
                   .FromAssembliesOf(typeof(IRequestHandler<>))
                   .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                       .AsImplementedInterfaces()
                       .WithTransientLifetime()
                   .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
                       .AsImplementedInterfaces()
                       .WithTransientLifetime());
            
            services.AddDbContext<OnlineStoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("OnlineStore"));
            });

            return services;
        }
    }
}
