using FluentValidation;

namespace Movieasy.Application.Movies.AddMovie
{
    public class AddMovieCommandValidator : AbstractValidator<AddMovieCommand>
    {
        public AddMovieCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();

            RuleFor(x => x.Description).NotEmpty();

            RuleFor(x => x.Duration).NotEmpty();

            RuleFor(x => x.Rating).NotEmpty();
        }
    }
}
