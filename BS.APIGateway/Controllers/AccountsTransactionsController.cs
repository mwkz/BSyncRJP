using AutoMapper;
using BS.APIGateway.Extensions;
using BS.APIGateway.ViewModels;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace BS.APIGateway.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountsTransactionsController : ControllerBase
    {
        private readonly string address;
        private readonly string accountsAddress;
        private readonly IMapper mapper;

        public AccountsTransactionsController(IConfiguration configuration, IMapper mapper)
        {
            address = configuration.GetValue<string>("AccountsTransactionsGRPCAddress");
            accountsAddress = configuration.GetValue<string>("AccountsGRPCAddress");
            this.mapper = mapper;
        }

        [HttpGet("{accountId:int}")]
        public async Task<IActionResult> GetAccountTransactions(int accountId, CancellationToken token)
        {
            
            using (var channel = GrpcChannel.ForAddress(address))
            {
                var client = new BS.Transactions.Service.AccountsTransactions.AccountsTransactionsClient(channel);
                var request = new Transactions.Service.GetAccountTransactionsRequest() { AccountId = accountId, UserId = HttpContext.User.GetUserId() };
                var result = await client.GetAccountTransactionsAsync(request, cancellationToken: token);
                result.UserId = request.UserId;

                return result == null ? NotFound() : Ok(mapper.Map<IEnumerable<Transactions.Service.GetAccountTransactionsResponse.Types.GetAccountTransactionsResponseEntry>, IEnumerable<AccountTransaction>>(result.Transactions));
            }
        }

        [HttpGet("summary/{accountId:int}")]
        public async Task<IActionResult> GetAccountSummary(int accountId, CancellationToken token)
        {

            AccountSummary summary = new AccountSummary();

            using (var channel = GrpcChannel.ForAddress(accountsAddress))
            {
                var client = new BS.Accounts.Service.Account.AccountClient(channel);
                var result = await client.GetAccountAsync(new Accounts.Service.GetAccountRequest() { UserId = HttpContext.User.GetUserId(), AccountId = accountId });

                if (result == null)
                    return NotFound();

                var account = mapper.Map<BS.Accounts.Service.GetAccountReply, Account>(result);
                mapper.Map(account, summary);
            }

            using (var channel = GrpcChannel.ForAddress(address))
            {
                var client = new BS.Transactions.Service.AccountsTransactions.AccountsTransactionsClient(channel);
                var request = new Transactions.Service.GetAccountTransactionsRequest() { AccountId = accountId, UserId = HttpContext.User.GetUserId() };
                var result = await client.GetAccountTransactionsAsync(request, cancellationToken: token);
                

                if (result != null)
                {
                    summary.Transactions = mapper.Map<IEnumerable<Transactions.Service.GetAccountTransactionsResponse.Types.GetAccountTransactionsResponseEntry>, IEnumerable<AccountTransaction>>(result.Transactions);
                    
                }
            }

            return Ok(summary);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAccountTransaction([FromBody] NewTransaction newTransaction, CancellationToken token)
        {
            using (var channel = GrpcChannel.ForAddress(address))
            {
                var client = new BS.Transactions.Service.AccountsTransactions.AccountsTransactionsClient(channel);
                Transactions.Service.AddAccountTransactionRequest request = new();
                
                mapper.Map(newTransaction, request);
                request.UserId = HttpContext.User.GetUserId();

                var result = await client.AddAccountTransactionAsync(request, cancellationToken: token);

                if (result.Success == false)
                {
                    if (result.Success == false)
                    {
                        var problems = new ValidationProblemDetails();
                        problems.Errors.Add("Validation", result.Errors.ToArray());
                        return ValidationProblem(problems);
                    }
                }

                return Ok(mapper.Map<Transactions.Service.AddAccountTransactionResponse, AccountTransaction>(result));
            }
        }
    }
}
