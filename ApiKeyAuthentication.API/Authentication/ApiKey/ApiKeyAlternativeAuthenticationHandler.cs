using ApiKeyAuthentication.API.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ApiKeyAuthentication.API.Authentication.ApiKey
{
    public class ApiKeyAlternativeAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly IClientService _clientService;
        private readonly string _headerName;
        private readonly string _defaultSchema;
        public ApiKeyAlternativeAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IClientService clientService) : base(options, logger, encoder, clock)
        {
            _clientService = clientService;
            _headerName = ApiKeyAuthenticationOptions.HeaderName + "-alternative";
            _defaultSchema = ApiKeyAuthenticationOptions.DefaultScheme + "Alternative";
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(_headerName, out var apiKey) || apiKey.Count != 1)
            {
                return AuthenticateResult.Fail($"{_headerName} not provided");
            }

            var permissions = await _clientService.GetPermissionAsync(apiKey);

            if (!permissions.Any())
            {
                return AuthenticateResult.Fail("failed");
            }

            var claims = new List<Claim>();

            foreach (var permission in permissions)
            {
                claims.Add(new Claim(ClaimTypes.Role, permission));
            }

            var claimsIdentity = new ClaimsIdentity(claims, _defaultSchema);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var ticket = new AuthenticationTicket(claimsPrincipal, _defaultSchema);

            return AuthenticateResult.Success(ticket);
        }
    }
}
