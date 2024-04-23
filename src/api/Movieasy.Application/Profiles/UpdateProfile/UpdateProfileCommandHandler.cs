using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Users;

namespace Movieasy.Application.Profiles.UpdateProfile
{
    internal sealed class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public UpdateProfileCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
            {
                return Result.Failure(UserErrors.NotFound);
            }

            if(_userContext.UserId != user.Id)
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }

            user.Update(request.Details);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
