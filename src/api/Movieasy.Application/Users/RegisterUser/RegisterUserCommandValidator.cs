using FluentValidation;

namespace Movieasy.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().MinimumLength(3);

            RuleFor(u => u.LastName).NotEmpty().MinimumLength(3);

            RuleFor(u => u.Email).NotEmpty().EmailAddress();

            RuleFor(u => u.Password).NotEmpty().MinimumLength(5);
        }
    }
}
