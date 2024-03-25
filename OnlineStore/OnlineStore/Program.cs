using CorrelationId;
using CorrelationId.DependencyInjection;
using OnlineStore.Api.Middleware;
using OnlineStore.Api.Modules;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.HttpLogging;

namespace OnlineStore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApiVersioning(t =>
            {
                t.ApiVersionReader = new UrlSegmentApiVersionReader();
                t.ReportApiVersions = true;
            });
            builder.Services.AddSwaggerGen();
            builder.Services.AddCore(builder.Configuration);
            builder.Services.AddLogging();
            builder.Services.AddDefaultCorrelationId();
            builder.Services.AddHttpLogging(options =>
            {
                options.LoggingFields = HttpLoggingFields.All;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "Online Store API V1");
                    c.OAuthAppName("Online Store API");
                });
            }

            app.UseCorrelationId();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseHttpLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
