using AggregatorApi.Messaging.Producer.Client;
using AggregatorApi.Messaging.Producer.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AggregatorApi.Shared.Extensions;
using AggregatorApi.HttpClients;
using AggregatorApi.Settings;
using System.Text.Json;

namespace AggregatorApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureSettings<IRabbitMqSettings, RabbitMqSettings>(Configuration);
            services.ConfigureSettings<IExternalApiSettings, ExternalApiSettings>(Configuration);

            services.AddHttpClient<IContactHttpClient, ContactHttpClient>();
            services.AddHttpClient<IContactInformationHttpClient, ContactInformationHttpClient>();

            services.AddSingleton<IQueueProducer, QueueProducer>();

            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.WriteIndented = true;
                        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AggregatorApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AggregatorApi v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
