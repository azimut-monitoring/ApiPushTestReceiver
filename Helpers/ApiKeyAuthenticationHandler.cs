using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiPushTestReceiver.Helpers
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string AuthenticationScheme = "ApiKey";

        private readonly string _apiKey;
        private readonly string _headerName;

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOptions<AppSettings> settings)
            : base(options, logger, encoder, clock)
        {
            _headerName = settings.Value.ApiKeyHeaderName;
            _apiKey = settings.Value.ValidApiKey;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(_headerName))
                return Task.FromResult(AuthenticateResult.Fail($"Missing {_headerName} Header"));

            if(Request.Headers[_headerName] != _apiKey)
                return Task.FromResult(AuthenticateResult.Fail("Invalid API Key"));

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, _apiKey),
                new Claim(ClaimTypes.Name, _apiKey),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
