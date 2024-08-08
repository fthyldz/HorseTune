using Application.Abstractions.Infrastructure;
using Domain.Events;
using Domain.Primitives;
using Infrastructure.FileStorages;
using Infrastructure.MessageBuses;
using Infrastructure.MessageBuses.Consumers;
using Infrastructure.Settings;
using MassTransit;
using MassTransit.RabbitMqTransport.Topology;
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
            // x.AddConsumer<ArtistImageProcessedConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                
                cfg.Publish<BaseEvent>(y => y.Exclude = true);
                
                cfg.Publish<IntegrationEvent>(y => y.Exclude = true);
                cfg.UseJsonSerializer();
                cfg.Message<ArtistImageUploadedEvent>(y =>
                {
                    y.SetEntityName("artist_image_uploaded");
                });
                
                cfg.Publish<ArtistImageUploadedEvent>(y =>
                {
                    y.Durable = true;
                    y.ExchangeType = "direct";
                    y.BindQueue("artist_image_uploaded", "artist_image_uploaded", config =>
                    {
                        config.ExchangeType = "direct";
                        config.Durable = true;
                        config.RoutingKey = "artist_image_uploaded";
                    });
                });
                
                /*cfg.ReceiveEndpoint("artist_image_uploaded", e =>
                {
                    e.ConfigureConsumeTopology = false;
                    e.ExchangeType = "direct";
                    e.Durable = true;
                    e.BindQueue = true;
                    
                    /*e.Bind("artist_image_uploaded_event", y =>
                    {
                        y.RoutingKey = "artist_image_uploaded_event";
                        y.ExchangeType = "direct";
                        y.Durable = true;
                    });
                });*/
                
                /*cfg.ReceiveEndpoint("artist_image_uploaded_queue", e =>
                {
                    //e.ConfigureConsumeTopology = false; // Otomatik topoloji yapılandırmasını kapatın
                    e.ExchangeType = "direct";
                    e.Durable = true;
                    // Kuyruğu exchange ile bağlayın
                    e.Bind("artist_image_uploaded", y =>
                    {
                        y.RoutingKey = "artist_image_uploaded"; // Routing key
                        y.ExchangeType = "direct"; // Exchange tipini direct olarak ayarlayın
                        y.Durable = true; // Kuyruğun kalıcı olması için
                    });
                });*/
                
                //cfg.ConfigureEndpoints(context);
            });
        });
        services.AddTransient<IMessageBus, MessageBus>();
        
        return services;
    }
}