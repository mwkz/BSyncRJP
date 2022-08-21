using BS.Security.Infrastructure.Extensions;
using BS.Security.Infrastructure.Repositories.DBContexts;
using BS.Secutiry.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682


builder.Services.AddGrpc();
builder.Services.AddSecurity(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DbContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.MapGrpcService<SecurityService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();
