using System.Security.Claims;

namespace BS.APIGateway.Extensions
{

    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                throw new InvalidOperationException();
            else
                return int.Parse(claim.Value);
        }
    }
}
