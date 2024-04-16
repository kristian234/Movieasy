using FluentValidation;
using Movieasy.Domain.Actors;

namespace Movieasy.Application.Actors.UpdateActor
{
    internal class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(ActorConstants.NameMaxLength);

            RuleFor(x => x.Biography).MaximumLength(ActorConstants.BiographyMaxLength);
        }
    }
}
