using BS.APIGateway.ViewModels;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BS.APIGateway.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly string address;
        private readonly ConfigurationManager configuration;

        public SecurityController(ConfigurationManager configuration)
        {
            
            address = configuration["SecurityTransactionsGRPCAddress"];
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request, CancellationToken cancellationToken)
        {
            AuthenticationResult user;

            using (var channel = GrpcChannel.ForAddress(address))
            {
                var client = new BS.Security.Service.Security.SecurityClient(channel);
                var response = await client.AuthenticaUserAsync(new Security.Service.AuthenticateUserRequest()
                                { Username = request.Username, Password = request.Password }, cancellationToken: cancellationToken);

                if (response == null || response.Authenticated != true || response.Id.HasValue != true)
                    return Unauthorized();
                else
                    user = new AuthenticationResult()
                    {
                        Name = response.Username,    
                        Id = response.Id.Value
                    };                    
            }

            var issuer = configuration["Jwt:Issuer"];

            var audience = configuration["Jwt:Audience"];

            var key = Encoding.ASCII.GetBytes (configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var result = tokenHandler.WriteToken(token);

            return Ok(result);
            
        }
}
}
