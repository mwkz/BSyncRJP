using BS.Accounts.Infrastructure.Extensions;
using BS.Accounts.Infrastructure.Repositories.DBContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682


builder.Services.AddGrpc();
builder.Services.AddAccounts(builder.Configuration);

builder.Services.AddAutoMapper(
        typeof(BS.Accounts.Service.Profiles.AccountsProfile).Assembly,
        typeof(BS.Accounts.Infrastructure.Profiles.AccountProfile).Assembly);


var app = builder.Build();
// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DbContext>();
    dataContext.Database.Migrate();
}

app.MapGrpcService<BS.Accounts.Service.Services.AccountsService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
