using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Actors;

namespace Movieasy.Application.Actors.AddActor
{
    internal sealed class AddActorCommandHandler : ICommandHandler<AddActorCommand, Guid>
    {
        private readonly IActorRepository _actorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddActorCommandHandler(
            IActorRepository actorRepository,
            IUnitOfWork unitOfWork)
        {
            _actorRepository = actorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddActorCommand request, CancellationToken cancellationToken)
        {
            Name actorName = new Name(request.Name);
            Biography actorBiography = new Biography(request.Biography);

            Actor actor = Actor.Create(actorName, actorBiography);

            await _actorRepository.AddAsync(actor);
            await _unitOfWork.SaveChangesAsync();

            return actor.Id;
        }
    }
}
