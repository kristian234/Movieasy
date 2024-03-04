using FluentValidation;

namespace Movieasy.Application.Movies.UpdateMovie
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();

            RuleFor(x => x.Description).NotEmpty();

            RuleFor(x => x.Duration).NotEmpty();

            RuleFor(x => x.Rating).NotEmpty();
        }
    }
}
