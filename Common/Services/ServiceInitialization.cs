using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Services;

public static class ServiceInitialization
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton(services =>
        {
            // Get the service address from appsettings.json
            var config = services.GetRequiredService<IConfiguration>();
            var backendUrl = config["BackendUrl"];

            // If no address is set then fallback to the current webpage URL
            if (string.IsNullOrEmpty(backendUrl))
            {
                var navigationManager = services.GetRequiredService<NavigationManager>();
                backendUrl = navigationManager.BaseUri;
            }

            // Create a channel with a GrpcWebHandler that is addressed to the backend server.
            //
            // GrpcWebText is used because server streaming requires it. If server streaming is not used in your app
            // then GrpcWeb is recommended because it produces smaller messages.
            var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());

            return GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions { HttpHandler = httpHandler });
        });

        return services;
    }
}
