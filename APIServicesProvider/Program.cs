using Domain.Exceptions;
using Infrastructure.DBProvider;
using ServicesProvider.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.LoadServices();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1.1");
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseOutputCache();

//mapping endpoints
app.MapUserEndpoint();
app.MapScheduleEndpoint();
app.MapTaskEndpoint();
app.MapBarberShopEndpoint();

app.Run();
