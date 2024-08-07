using Application.Abstractions.Infrastructure;
using Infrastructure.FileStorages;
using Infrastructure.MessageBuses;
using Infrastructure.MessageBuses.Consumers;
using Infrastructure.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var minioSetting = configuration.GetSection("MinIO").Get<MinioSetting>();
        services.Configure<MinioSetting>(configuration.GetSection("MinIO"));
        services.AddMinio(config =>
            config.WithSSL(false).WithEndpoint(minioSetting?.Endpoint)
                .WithCredentials(minioSetting?.AccessKey, minioSetting?.SecretKey));
        services.AddScoped<IFileStorageService, MinioFileStorageService>();
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ArtistImageProcessedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("admin");
                    h.Password("admin123");
                });
                
                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddTransient<IMessageBus, MessageBus>();
        
        return services;
    }
}