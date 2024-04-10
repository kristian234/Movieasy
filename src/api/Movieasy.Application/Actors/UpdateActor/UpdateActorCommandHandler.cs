using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Actors;

namespace Movieasy.Application.Actors.UpdateActor
{
    internal sealed class UpdateActorCommandHandler : ICommandHandler<UpdateActorCommand>
    {
        private readonly IActorRepository _actorRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateActorCommandHandler(
            IActorRepository actorRepository,
            IUnitOfWork unitOfWork)
        {
            _actorRepository = actorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateActorCommand request, CancellationToken cancellationToken)
        {
            Actor? actor = await _actorRepository.GetByIdAsync(request.ActorId);

            if (actor == null)
            {
                return Result.Failure(ActorErrors.NotFound);
            }

            Result updateResult = actor.Update(
                request.Name,
                request.Biography);

            if (updateResult.IsFailure)
            {
                return updateResult;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
