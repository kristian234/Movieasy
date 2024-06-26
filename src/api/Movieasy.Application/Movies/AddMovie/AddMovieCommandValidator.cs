﻿using FluentValidation;
using Movieasy.Domain.Movies;

namespace Movieasy.Application.Movies.AddMovie
{
    internal class AddMovieCommandValidator : AbstractValidator<AddMovieCommand>
    {
        public AddMovieCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(MovieConstants.TitleMaxLength);

            RuleFor(x => x.Description).NotEmpty().MaximumLength(MovieConstants.DescriptionMaxLength);

            RuleFor(x => x.TrailerUrl).NotEmpty().MaximumLength(MovieConstants.TrailerMaxLength);

            RuleFor(x => x.Duration).NotEmpty();

            RuleFor(x => x.Rating).NotEmpty()
                .Must(x => Enum.IsDefined(typeof(Rating), x))
                .WithMessage("Invalid rating value.");
        }
    }
}
