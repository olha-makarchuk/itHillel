using Persistence.Context;
using EntityFrameworkCore.Testing.Common.Helpers;
using EntityFrameworkCore.Testing.Moq.Helpers;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace MovieManager.Api.IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IWebHostEnvironment env)
            : base(configuration, env)
        {
        }

        protected override void ConfigureDb(IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            var context = ConfigureDb<ApplicationDbContext>().MockedDbContext;
            services.AddSingleton<ApplicationDbContext>(c => context);
        }

        private IMockedDbContextBuilder<T> ConfigureDb<T>()
                where T : DbContext
        {
            var options = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContextToMock = (T?)Activator.CreateInstance(typeof(T), options)!;
            return new MockedDbContextBuilder<T>()
                .UseDbContext(dbContextToMock)
                .UseConstructorWithParameters(options);
        }
    }
}
