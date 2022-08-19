using BS.Accounts.Infrastructure.Repositories.DBContexts;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AccountsDBContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("MainDBConnection"));
});


// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

builder.Services.AddGrpc();

var app = builder.Build();
// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
//app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
