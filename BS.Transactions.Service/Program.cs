using BS.Accounts.Infrastructure.Extensions;
using BS.Transactions.Infrastructure.Repositories.DBContexts;
using BS.Transactions.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682



// Configure the HTTP request pipeline.

builder.Services.AddAccountsTransactions(builder.Configuration);
builder.Services.AddGrpc();

builder.Services.AddAutoMapper(
        typeof(BS.Transactions.Service.Profiles.AccountTransactionProfile).Assembly,
        typeof(BS.Transactions.Infrastructure.Profiles.AccountTransactionProfile).Assembly);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DbContext>();
    dataContext.Database.Migrate();
}


// Configure the HTTP request pipeline.
app.MapGrpcService<AccountsTransactionsService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
