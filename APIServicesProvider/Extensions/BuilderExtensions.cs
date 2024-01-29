using System.Text.Json.Serialization;

namespace ServicesProvider.Extensions;

public static class BuilderExtensions
{
    public static void LoadServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddOutputCache(opt =>
        {
            opt.DefaultExpirationTimeSpan = TimeSpan.FromMinutes(5);
        });
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.ConfigureHttpJsonOptions(options 
            => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    }
}