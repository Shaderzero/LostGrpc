using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Services;

public static class ServiceInitialization
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton(services =>
        {
            var backendUrl = CommonSettings.BackendUrl;

            // GrpcWebText поддерживает стриминг, но GrpcWeb выдает пакеты меньше, работает с обоими
            var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
            return GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions { HttpHandler = httpHandler });
        });

        return services;
    }
}
