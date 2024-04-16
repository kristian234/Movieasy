using FluentValidation;
using Movieasy.Domain.Reviews;

namespace Movieasy.Application.Reviews.AddReview
{
    internal class AddReviewCommandValidator : AbstractValidator<AddReviewCommand>
    {
        public AddReviewCommandValidator()
        {
            RuleFor(x => x.Comment).NotEmpty().MaximumLength(ReviewConstants.CommentMaxLength);

            RuleFor(x => x.Rating)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);
        }
    }
}
