using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Users.Events;
using System.Collections.Immutable;

namespace Movieasy.Domain.Users
{
    public sealed class User : Entity
    {
        private readonly List<Role> _roles = new List<Role>();

        private User(
            Guid id,
            FirstName firstName,
            LastName lastName,
            Email email
            )
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }

        public string IdentityId { get; private set; } = string.Empty;
        public IReadOnlyCollection<Role> Roles => _roles.ToImmutableList();
        public static User Create(FirstName firstName, LastName lastName, Email email)
        {
            User user = new User(Guid.NewGuid(), firstName, lastName, email);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            user._roles.Add(Role.Registered);

            return user;
        }

        public void SetIdentityId(string identityId)
        {
            IdentityId = identityId;
        }
    }
}
