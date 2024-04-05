using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Reviews.GetReview;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Reviews;
using System.Globalization;

namespace Movieasy.Application.Reviews.GetUserReviewForMovie
{
    internal sealed class GetUserReviewForMovieQueryHandler
        : IQueryHandler<GetUserReviewForMovieQuery, ReviewResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;

        public GetUserReviewForMovieQueryHandler(
            IApplicationDbContext context,
            IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result<ReviewResponse>> Handle(GetUserReviewForMovieQuery request, CancellationToken cancellationToken)
        {
            Guid userId = _userContext.UserId;

            var review = await _context.Reviews
                 .Include(r => r.User) 
                 .FirstOrDefaultAsync(r => r.MovieId == request.MovieId && r.UserId == userId);

            if (review == null)
            {
                return Result.Failure<ReviewResponse>(ReviewErrors.NotFound);
            }

            var response = new ReviewResponse()
            {
                Id = review.Id.ToString(),
                Comment = review.Comment.Value,
                ReviewerName = $"{review.User.FirstName.Value} {review.User.LastName.Value}",
                Rating = review.Rating.Value,
                CreatedOnDate = review.CreatedOnUtc.ToString("d", CultureInfo.InvariantCulture)
            };

            return response;
        }
    }
}
