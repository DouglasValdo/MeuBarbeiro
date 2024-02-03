namespace MobileUI.Service;

public static class AppApiHttpClient
{
    private static HttpClient _httpClient;

    public static HttpClient GetClient()
    {
        const string BaseAddress = "http://nhabarberu.cloud/api/";
        
        _httpClient ??= new HttpClient(GetPlatformMessageHandler());

        _httpClient.BaseAddress = new Uri(BaseAddress);

        return _httpClient;
    }

    //used just to access my localhost api
    private static HttpMessageHandler GetPlatformMessageHandler()
    {
#if ANDROID
        var handler = new Xamarin.Android.Net.AndroidMessageHandler();

        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            // if (cert is { Issuer: "CN=localhost" })
            return true;
            // return errors == System.Net.Security.SslPolicyErrors.None;
        };
        return handler;
#endif
        return null;
    }
}