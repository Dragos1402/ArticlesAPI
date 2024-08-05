using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;

namespace ArticlesApi.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization header missing.");

            var authHeader = Request.Headers["Authorization"].ToString();
            if (!authHeader.StartsWith("Basic "))
                return AuthenticateResult.Fail("Invalid authorization header.");

            var token = authHeader.Substring("Basic ".Length).Trim();
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(':', 2);

            if (credentials.Length != 2)
                return AuthenticateResult.Fail("Invalid authorization header.");

            var username = credentials[0];
            var password = credentials[1];

            // Validate credentials here (e.g., check against a database or hardcoded values)
            if (username != "user" || password != "password")
                return AuthenticateResult.Fail("Invalid username or password.");

            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
    public static class BasicAuthenticationDefaults
    {
        public const string AuthenticationScheme = "Basic";
    }

}
