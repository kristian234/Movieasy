using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Reviews;

namespace Movieasy.Application.Reviews.UpdateReview
{
    internal sealed class UpdateReviewCommandHandler : ICommandHandler<UpdateReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReviewCommandHandler(
            IReviewRepository reviewRepository,
            IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            Review? review = await _reviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);

            if (review == null)
            {
                return Result.Failure(ReviewErrors.NotFound);
            }

            Result reviewResult = review.Update(
                request.Comment,
                request.Rating);

            if (reviewResult.IsFailure)
            {
                return Result.Failure(reviewResult.Error);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
