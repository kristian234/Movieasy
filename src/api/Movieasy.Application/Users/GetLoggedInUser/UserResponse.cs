﻿namespace Movieasy.Application.Users.GetLoggedInUser
{
    public sealed class UserResponse
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
    }
}
