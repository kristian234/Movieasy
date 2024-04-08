using FluentValidation;
using Movieasy.Domain.Users;

namespace Movieasy.Application.Users.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty()
                .MinimumLength(UserConstants.FirstNameMinLength)
                .MaximumLength(UserConstants.FirstNameMaxLength);

            RuleFor(u => u.LastName).NotEmpty()
                .MinimumLength(UserConstants.LastNameMinLength)
                .MaximumLength(UserConstants.LastNameMaxLength);

            RuleFor(u => u.Email).NotEmpty().EmailAddress().MaximumLength(UserConstants.EmailMaxLength);

            RuleFor(u => u.Password).NotEmpty()
                .MinimumLength(UserConstants.PasswordMinLength)
                .MaximumLength(UserConstants.PasswordMaxLength);
        }
    }
}
