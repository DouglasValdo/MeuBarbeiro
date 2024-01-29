using Domain.Exceptions;
using Infrastructure.DBProvider;
using ServicesProvider.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.LoadServices();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseOutputCache();

//Retrieve the dbContext and pass it to the endpoints
var connectionString = app.Configuration.GetConnectionString("AppConnectionString");

if (string.IsNullOrWhiteSpace(connectionString)) 
    throw new NoConnectionStringProvidedException();

var dbContext = new DbContextProvider().GetDbContext(connectionString);
//mapping endpoints
app.MapUserEndpoint(dbContext);
app.MapScheduleEndpoint(dbContext);
app.MapTaskEndpoint(dbContext);

app.Run();
