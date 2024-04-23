using FluentValidation;
using Movieasy.Domain.Reviews;

namespace Movieasy.Application.Reviews.GetReview
{
    internal class GetReviewsQueryValidator : AbstractValidator<GetReviewsQuery>
    {
        public GetReviewsQueryValidator()
        {
            RuleFor(x => x.Rating)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);

            RuleFor(x => x.PageSize)
                .LessThanOrEqualTo(ReviewConstants.MaxPageSize);
        }
    }
}
