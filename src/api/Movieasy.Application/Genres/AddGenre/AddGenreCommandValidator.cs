using FluentValidation;
using Movieasy.Domain.Genres;

namespace Movieasy.Application.Genres.AddGenre
{
    public class AddGenreCommandValidator : AbstractValidator<AddGenreCommand>
    {
        public AddGenreCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(GenreConstants.NameMaxLength);
        }
    }
}
