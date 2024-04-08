using FluentValidation;
using Movieasy.Domain.Movies;

namespace Movieasy.Application.Movies.UpdateMovie
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(MovieConstants.TitleMaxLength);

            RuleFor(x => x.Description).NotEmpty().MaximumLength(MovieConstants.DescriptionMaxLength);

            RuleFor(x => x.Duration).NotEmpty();

            RuleFor(x => x.Rating).NotEmpty()
                .Must(x => Enum.IsDefined(typeof(Rating), x))
                .WithMessage("Invalid rating value.");
        }
    }
}
