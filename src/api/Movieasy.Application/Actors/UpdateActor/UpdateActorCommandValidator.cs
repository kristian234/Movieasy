using FluentValidation;
using Movieasy.Domain.Actors;

namespace Movieasy.Application.Actors.UpdateActor
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(ActorConstants.NameMaxLength);

            RuleFor(x => x.Biography).MaximumLength(ActorConstants.BiographyMaxLength);
        }
    }
}
