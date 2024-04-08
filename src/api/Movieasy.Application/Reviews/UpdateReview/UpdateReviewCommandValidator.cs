using FluentValidation;
using Movieasy.Domain.Reviews;

namespace Movieasy.Application.Reviews.UpdateReview
{
    internal sealed class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(x => x.Comment).NotEmpty().MaximumLength(ReviewConstants.CommentMaxLength);

            RuleFor(x => x.Rating)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);
        }
    }
}
