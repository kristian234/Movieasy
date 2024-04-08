using FluentValidation;
using Movieasy.Domain.Genres;

namespace Movieasy.Application.Genres.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(GenreConstants.NameMaxLength);
        }
    }
}
