﻿using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Movieasy.Infrastructure.Authentication.Models
{
    internal static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal? principal)
        {
            string? userId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            return Guid.TryParse(userId, out Guid parsedUserId) ?
                parsedUserId :
                throw new ApplicationException("User Id is unavailable");
        }

        public static string GetIdentityId(this ClaimsPrincipal? principal)
        {
            return principal?.FindFirstValue(ClaimTypes.NameIdentifier) ??
                throw new ApplicationException("User identity is unavailable");
        }
    }
}
