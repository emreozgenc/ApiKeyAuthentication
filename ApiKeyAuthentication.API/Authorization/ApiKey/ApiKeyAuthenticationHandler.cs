using ApiKeyAuthentication.API.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ApiKeyAuthentication.API.Authorization.ApiKey
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly IClientService _clientService;
        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IClientService clientService) : base(options, logger, encoder, clock)
        {
            _clientService = clientService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.TryGetValue(ApiKeyAuthenticationOptions.HeaderName, out var apiKey) || apiKey.Count != 1)
            {
                return AuthenticateResult.Fail($"{ApiKeyAuthenticationOptions.HeaderName} not provided");
            }

            var permissions = await _clientService.GetPermissionAsync(apiKey);
            
            if(!permissions.Any())
            {
                return AuthenticateResult.Fail("failed");
            }

            var claims = new List<Claim>();

            foreach(var permission in permissions)
            {
                claims.Add(new Claim(ClaimTypes.Role, permission));
            }

            var claimsIdentity = new ClaimsIdentity(claims, ApiKeyAuthenticationOptions.DefaultSchema);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var ticket = new AuthenticationTicket(claimsPrincipal, ApiKeyAuthenticationOptions.DefaultSchema);

            return AuthenticateResult.Success(ticket);
        }
    }

    public static class ApiKeyAuthenticationExtensions
    {
        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder builder, Action<ApiKeyAuthenticationOptions> options) 
        {
            return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultSchema, options);
        }

        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder builder)
        {
            return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultSchema, null!);
        }
    }
}
