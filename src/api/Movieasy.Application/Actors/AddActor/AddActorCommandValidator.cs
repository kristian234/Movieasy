using FluentValidation;
using Movieasy.Domain.Actors;

namespace Movieasy.Application.Actors.AddActor
{
    public class AddActorCommandValidator : AbstractValidator<AddActorCommand>
    {
        public AddActorCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(ActorConstants.NameMaxLength);

            RuleFor(x => x.Biography).MaximumLength(ActorConstants.BiographyMaxLength);
        }
    }
}
