// Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRNotificationApp.Hubs;

namespace SignalRNotificationApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.ConfigureServices((context, services) =>
                    {
                        services.AddCors(options =>
                        {
                            options.AddPolicy("CorsPolicy",
                                builder => builder
                                    .WithOrigins("http://localhost:3000") // Replace with your React app URL
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials()
                            );
                        });
                        services.AddControllers(); // Ensure AddControllers is called
                        services.AddSignalR(); // Add SignalR service
                        // other services if needed
                    });

                    webBuilder.Configure((app) =>
                    {
                        app.UseCors("CorsPolicy");
                        app.UseRouting();
                        // Map SignalR hub endpoint
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers(); // Map controllers
                            endpoints.MapHub<NotificationHub>("/NotificationHub"); // Map SignalR hub
                        });

                        // other middleware configurations
                    });
                });
    }
}
