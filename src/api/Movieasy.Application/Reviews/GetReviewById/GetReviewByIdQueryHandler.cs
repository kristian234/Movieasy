using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Movies.GetMovieById;
using Movieasy.Application.Reviews.GetReview;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Reviews;
using System.Globalization;

namespace Movieasy.Application.Reviews.GetReviewById
{
    internal sealed class GetReviewByIdQueryHandler : IQueryHandler<GetReviewByIdQuery, ReviewResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewByIdQueryHandler(
            IApplicationDbContext context,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ReviewResponse>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            ReviewResponse? review = await _context
                .Reviews
                .Where(r => r.Id == request.ReviewId)
                .Select(r => new ReviewResponse()
                {
                    Id = r.Id.ToString(),
                    Comment = r.Comment.Value,
                    CreatedOnDate = r.CreatedOnUtc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Rating = r.Rating.Value,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if(review == null)
            {
                return Result.Failure<ReviewResponse>(ReviewErrors.NotFound);
            }

            return review;
        }
    }
}
