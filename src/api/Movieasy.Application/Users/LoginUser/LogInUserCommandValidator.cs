using FluentValidation;
using Movieasy.Domain.Users;

namespace Movieasy.Application.Users.LoginUser
{
    public class LogInUserCommandValidator : AbstractValidator<LogInUserCommand>
    {
        public LogInUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(UserConstants.EmailMaxLength);

            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(UserConstants.PasswordMinLength)
                .MaximumLength(UserConstants.PasswordMaxLength);

            RuleFor(x => x.RememberMe).NotEmpty();
        }
    }
}
