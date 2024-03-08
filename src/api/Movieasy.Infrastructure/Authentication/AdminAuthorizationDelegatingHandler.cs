using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Movieasy.Infrastructure.Authentication.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Movieasy.Infrastructure.Authentication
{
    public sealed class AdminAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly KeycloakOptions _keycloakOptions;
        public AdminAuthorizationDelegatingHandler(IOptions<KeycloakOptions> keycloakOptions)
        {
            _keycloakOptions = keycloakOptions.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            AuthorizationToken authorizationToken = await GetAuthorizationTokenAsync(cancellationToken);

            request.Headers.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                authorizationToken.AccessToken);

            HttpResponseMessage httpResponseMessage = await base.SendAsync(request, cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return httpResponseMessage;
        }

        private async Task<AuthorizationToken> GetAuthorizationTokenAsync(CancellationToken cancellationToken)
        {
            KeyValuePair<string, string>[] authorizationRequestParameters =
            {
                new("client_id", _keycloakOptions.AdminClientId),
                new("client_secret", _keycloakOptions.AdminClientSecret),
                new("scope", "openid email"),
                new("grant_type", "client_credentials")
            };

            var authorizationRequestContent = new FormUrlEncodedContent(authorizationRequestParameters);

            using var authorizationRequest = new HttpRequestMessage(
                       HttpMethod.Post,
                       new Uri(_keycloakOptions.TokenUrl))
            {
                Content = authorizationRequestContent
            };

            HttpResponseMessage authorizationResponse = await base.SendAsync(authorizationRequest, cancellationToken);

            authorizationResponse.EnsureSuccessStatusCode();

            return await authorizationResponse.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken) ??
                   throw new ApplicationException();
        }

    }
}
