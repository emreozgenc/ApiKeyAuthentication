using Microsoft.AspNetCore.Authentication;

namespace ApiKeyAuthentication.API.Authorization.ApiKey
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string HeaderName = "x-api-key";
        public const string DefaultSchema = "XApiKey";
    }
}
