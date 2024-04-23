using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Reviews;

namespace Movieasy.Application.Reviews.DeleteReview
{
    internal sealed class DeleteReviewCommandHandler : ICommandHandler<DeleteReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReviewCommandHandler(
            IReviewRepository reviewRepository,
            IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            Review? review = await _reviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);

            if (review == null)
            {
                return Result.Failure(ReviewErrors.NotFound);
            }

            _reviewRepository.Remove(review);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
