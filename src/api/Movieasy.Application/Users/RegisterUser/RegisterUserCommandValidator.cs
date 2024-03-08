using FluentValidation;

namespace Movieasy.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();

            RuleFor(u => u.LastName).NotEmpty();

            RuleFor(u => u.Email).NotEmpty();

            RuleFor(u => u.Password).NotEmpty().MinimumLength(5);
        }
    }
}
