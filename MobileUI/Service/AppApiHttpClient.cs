namespace MobileUI.Service;

public static class AppApiHttpClient
{
    private static HttpClient? _httpClient;

    public static HttpClient GetClient()
    {
        const string baseAddress = "http" + "://nhabarberu.cloud/api/";

        if (_httpClient != null) return _httpClient;
        
        _httpClient = new HttpClient();

        _httpClient.BaseAddress = new Uri(baseAddress);
        
        return _httpClient;
    }
}