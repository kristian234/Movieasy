using FluentValidation;

namespace Movieasy.Application.Users.LoginUser
{
    internal sealed class LogInUserCommandValidator : AbstractValidator<LogInUserCommand>
    {
        public LogInUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).NotEmpty().MinimumLength(5);

            RuleFor(x => x.RememberMe).NotEmpty();
        }
    }
}
