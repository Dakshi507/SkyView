//For service discovery using consul
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common
{
    public static class AppExtensions
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient>(p =>
            {
                var consulConfig = new ConsulClientConfiguration();
                var address = configuration["Consul:ConsulAddress"];
                consulConfig.Address = new Uri(address);

                return new ConsulClient(consulConfig);
            });

            return services;
        }
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("ConsulRegistration");
            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            var registration = new AgentServiceRegistration
            {
                ID = configuration["Consul:ServiceId"],
                Name = configuration["Consul:ServiceName"],
                Address = configuration["Consul:ServiceHost"],
                Port = Convert.ToInt32(configuration["Consul:ServicePort"])
            };

            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Unregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            });

            return app;
        }
    }

}
