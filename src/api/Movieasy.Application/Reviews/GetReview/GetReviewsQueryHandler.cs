using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Movies.GetMovie;
using Movieasy.Application.Movies.GetMovieById;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Reviews;
using System.Globalization;

namespace Movieasy.Application.Reviews.GetReview
{
    internal sealed class GetReviewsQueryHandler : IQueryHandler<GetReviewsQuery, PagedList<ReviewResponse>>
    {
        private readonly IApplicationDbContext _context;
        public GetReviewsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<ReviewResponse>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Review> reviewsQuery = _context.Reviews;

            var reviewResponseQuery = reviewsQuery
                .Select(r => new ReviewResponse()
                {
                    Id = r.Id.ToString(),
                    Comment = r.Comment.Value,
                    Rating = r.Rating.Value,
                    CreatedOnDate = r.CreatedOnUtc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                })
                .AsNoTracking();

            var reviews = await PagedList<ReviewResponse>.CreateAsync(
                reviewResponseQuery,
                request.Page,
                request.PageSize);

            return reviews;
        }
    }
}
