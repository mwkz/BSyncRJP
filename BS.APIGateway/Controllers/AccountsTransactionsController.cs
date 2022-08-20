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
        private readonly IMapper mapper;

        public AccountsTransactionsController(IConfiguration configuration, IMapper mapper)
        {
            address = configuration.GetValue<string>("AccountsTransactionsGRPCAddress");
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

                return result == null ? NotFound() : Ok(mapper.Map<AccountTransaction>(result));
            }
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

                return Ok(mapper.Map<Transactions.Service.AddAccountTransactionResponse, NewTransaction>(result));
            }
        }
    }
}
