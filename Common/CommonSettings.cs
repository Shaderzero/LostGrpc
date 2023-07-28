namespace Common;

public enum DevicePlatform
{
    Android,
    Blazor
}

public static class CommonSettings
{
    private static DevicePlatform? _devicePlatform;

    public static string BackendUrl => CurrentDevicePlatform == DevicePlatform.Android
    //? "http://10.0.2.2:5000"
    ? "http://10.0.2.2:5000"
    : "https://localhost:5001";

    public static DevicePlatform CurrentDevicePlatform
        => _devicePlatform
        ?? throw new InvalidOperationException("before app start it's necessary to call CommonSettings.SetCurrentDevicePlatform");

    public static void SetCurrentDevicePlatform(DevicePlatform devicePlatform)
        => _devicePlatform = devicePlatform;
}
