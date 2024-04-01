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

            if (request.Rating != null)
            {
                reviewsQuery = reviewsQuery.Where(r =>
                    ((int)r.Rating) == request.Rating);
            }

            var reviewResponseQuery = reviewsQuery
                .Where(r => r.MovieId == request.MovieId)
                .Include(r => r.User)
                .Select(r => new ReviewResponse()
                {
                    Id = r.Id.ToString(),
                    Comment = r.Comment.Value,
                    ReviewerName = $"{r.User.FirstName.Value} {r.User.LastName.Value}",
                    Rating = r.Rating.Value,
                    CreatedOnDate = r.CreatedOnUtc.ToString("d", CultureInfo.InvariantCulture)
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
