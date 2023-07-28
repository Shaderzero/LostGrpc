using Common;
using Common.Services;
using Microsoft.Extensions.Logging;

namespace Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

#if ANDROID
        CommonSettings.SetCurrentDevicePlatform(Common.DevicePlatform.Android);
#else
        CommonSettings.SetCurrentDevicePlatform(Common.DevicePlatform.Blazor);
#endif


            builder.Services.AddCommonServices();

            return builder.Build();
        }
    }
}