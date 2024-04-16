using FluentValidation;
using Movieasy.Domain.Genres;

namespace Movieasy.Application.Genres.UpdateGenre
{
    internal class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(GenreConstants.NameMaxLength);
        }
    }
}
