using FluentValidation;
using Movieasy.Domain.Genres;

namespace Movieasy.Application.Genres.AddGenre
{
    internal sealed class AddGenreCommandValidator : AbstractValidator<AddGenreCommand>
    {
        public AddGenreCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(GenreConstants.NameMaxLength);
        }
    }
}
