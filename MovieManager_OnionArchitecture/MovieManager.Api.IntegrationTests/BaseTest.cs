using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;
using Persistence.Context;

namespace MovieManager.Api.IntegrationTests
{
    public class BaseTest : IDisposable
    {
        protected IHost Host = null!;
        private IHostBuilder _server = null!;

        protected ApplicationDbContext AppDbContext = null!;

        public virtual HttpClient GetClient()
        {
            Host = _server.Start();
            AppDbContext = Host.Services.GetRequiredService<ApplicationDbContext>();
            return Host.GetTestClient();
        }

        protected BaseTest InitTestServer()
        {
            _server = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseEnvironment("Development")
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseStartup<TestStartup>();
                });
            return this;
        }

        public void Dispose()
        {
            StopServer();
        }

        private void StopServer()
        {
            Host?.StopAsync().GetAwaiter().GetResult();
        }
    }
}