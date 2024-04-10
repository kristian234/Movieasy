using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Actors;

namespace Movieasy.Application.Actors.DeleteActor
{
    internal sealed class DeleteActorCommandHandler : ICommandHandler<DeleteActorCommand>
    {
        private readonly IActorRepository _actorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteActorCommandHandler(
            IActorRepository actorRepository,
            IUnitOfWork unitOfWork)
        {
            _actorRepository = actorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
        {
            Actor? actor = await _actorRepository.GetByIdAsync(request.ActorId);

            if (actor == null)
            {
                return Result.Failure(ActorErrors.NotFound);
            }

            _actorRepository.Remove(actor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
