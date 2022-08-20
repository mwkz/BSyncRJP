using AutoMapper;
using BS.APIGateway.Extensions;
using BS.APIGateway.ViewModels;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BS.APIGateway.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly string address;
        private readonly IMapper mapper;

        public AccountsController(IConfiguration configuration, IMapper mapper)
        {
            address = configuration.GetValue<string>("AccountsGRPCAddress");
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] NewAccount newAccount, CancellationToken token)
        {
            using (var channel = GrpcChannel.ForAddress(address))
            {
                var client = new BS.Accounts.Service.Account.AccountClient(channel);
                var request = mapper.Map<NewAccount, BS.Accounts.Service.AddAccountRequest>(newAccount);
                request.UserId = HttpContext.User.GetUserId();

                var result = await client.AddAccountAsync(request);

                if (result.Success == false)
                {
                    var problems = new ValidationProblemDetails();
                    problems.Errors.Add("Validation", result.Errors.ToArray());
                    return ValidationProblem(problems);                    
                }

                return Ok(mapper.Map<BS.Accounts.Service.AddAccountReply, Account>(result));

            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccount(int id, CancellationToken token)
        {
            using (var channel = GrpcChannel.ForAddress(address))
            {
                var client = new BS.Accounts.Service.Account.AccountClient(channel);
                var result = await client.GetAccountAsync(new Accounts.Service.GetAccountRequest() {  UserId = HttpContext.User.GetUserId() , AccountId = id});
                
                if (result == null)
                    return NotFound();

                return Ok(mapper.Map<BS.Accounts.Service.GetAccountReply, Account>(result));
                
            }

        }

        [HttpGet]
        public async  Task<IActionResult> GetAccounts(CancellationToken token)
        {
            using (var channel = GrpcChannel.ForAddress(address))
            {
                var client = new BS.Accounts.Service.Account.AccountClient(channel);
                var result = await client.GetAccountsAsync(new Accounts.Service.GetAccountsRequest() { UserId = HttpContext.User.GetUserId() }, cancellationToken: token);
                return Ok(mapper.Map<IEnumerable<BS.Accounts.Service.GetAccountsReply.Types.GetAccountsReplyEntry>, IEnumerable<Account>>(result.Accounts));
            }
        }
    }
}
