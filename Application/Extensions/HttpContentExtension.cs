using Newtonsoft.Json;

namespace ApplicationStructure.Extensions;

public static class HttpContentExtension
{
    public static async Task<T?> GetFromJsonAsync<T>(this HttpContent content)
    {
        var stream    = await content.ReadAsStreamAsync();

        using var reader = new StreamReader(stream);
        using var jsonReader = new JsonTextReader(reader);
        
        var ser = new JsonSerializer();    
        
        return ser.Deserialize<T>(jsonReader);
    }
}