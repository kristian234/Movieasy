﻿using Movieasy.Infrastructure.Authentication.Models;
using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Domain.Users;
using System.Net.Http.Json;

namespace Movieasy.Infrastructure.Authentication
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private const string PasswordCredentialType = "password";

        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> RegisterAsync(
            User user,
            string password,
            CancellationToken cancellationToken = default)
        {
            var userRepresentationModel = UserRepresentationModel.FromUser(user);

            userRepresentationModel.Credentials = new Models.CredentialRepresentationModel[]
            {
                new Models.CredentialRepresentationModel()
                {
                    Value = password,
                    Temporary = false,
                    Type = PasswordCredentialType
                }
            };

            var response = await _httpClient.PostAsJsonAsync(
                "users",
                userRepresentationModel,
                cancellationToken);

            return ExtractIdentityIdFromLocationHeader(response);
        }

        private static string ExtractIdentityIdFromLocationHeader(
            HttpResponseMessage httpResponseMessage)
        {
            const string usersSegmentName = "users/";

            var locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

            if(locationHeader == null)
            {
                throw new InvalidOperationException("Location header cannot be null");
            }

            int userSegmentValueIndex = locationHeader.IndexOf(
                usersSegmentName,
                StringComparison.InvariantCultureIgnoreCase);

            string userIdentityId = locationHeader.Substring(
                userSegmentValueIndex + usersSegmentName.Length);

            return userIdentityId;
        }
    }
}
