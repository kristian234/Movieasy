using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Abstractions.Clock;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Reviews;

namespace Movieasy.Application.Reviews.AddReview
{
    internal sealed class AddReviewCommandHandler : ICommandHandler<AddReviewCommand, Guid>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public AddReviewCommandHandler(
            IReviewRepository reviewRepository,
            IMovieRepository movieRepository,
            IUserContext userContext,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _userContext = userContext;
            _movieRepository = movieRepository;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            // Check if such a movie even exists.
            Movie? movie = await _movieRepository
                .GetByIdAsync(request.MovieId, cancellationToken);

            if (movie == null)
            {
                return Result.Failure<Guid>(ReviewErrors.NotEligible);
            }

            // Check if there's such a review that exists.
            Guid userId = _userContext.UserId;
            Review? existingReview = await _reviewRepository.GetByUserAndMovieIdAsync(
                userId,
                request.MovieId,
                cancellationToken);

            if (existingReview != null)
            {
                return Result.Failure<Guid>(ReviewErrors.AlreadyPosted);
            }

            // Create the review
            var rating = Domain.Reviews.Rating.Create(request.Rating);

            if (rating.IsFailure)
            {
                return Result.Failure<Guid>(rating.Error);
            }

            Result<Review> reviewCreateResult = Review.Create(
                movie,
                userId,
                rating.Value,
                new Comment(request.Comment),
                _dateTimeProvider.UtcNow);

            if(reviewCreateResult.IsFailure)
            {
                return Result.Failure<Guid>(reviewCreateResult.Error);
            }

            await _reviewRepository.AddAsync(reviewCreateResult.Value);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reviewCreateResult.Value.Id;
        }
    }
}
