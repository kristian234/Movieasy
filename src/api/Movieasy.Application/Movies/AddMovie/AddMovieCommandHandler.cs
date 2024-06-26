﻿using Movieasy.Application.Abstractions.Clock;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Abstractions.Photos;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Actors;
using Movieasy.Domain.Genres;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;

namespace Movieasy.Application.Movies.AddMovie
{
    internal sealed class AddMovieCommandHandler : ICommandHandler<AddMovieCommand, Guid>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IPhotoRepository _photoRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IActorRepository _actorRepository;

        public AddMovieCommandHandler(
            IMovieRepository movieRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            IPhotoAccessor photoAccessor,
            IPhotoRepository photoRepository,
            IGenreRepository genreRepository,
            IActorRepository actorRepository)
        {
            _movieRepository = movieRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _photoAccessor = photoAccessor;
            _photoRepository = photoRepository;
            _genreRepository = genreRepository;
            _actorRepository = actorRepository;
        }

        public async Task<Result<Guid>> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            Result<Duration> movieDuration = Duration.Create(request.Duration);
            if (movieDuration.IsFailure)
            {
                return Result.Failure<Guid>(Duration.Invalid);
            }

            IEnumerable<Genre> genres = await _genreRepository.GetByIdsAsync(request.Genres);
            if (genres.Count() != request.Genres.Count)
            {
                return Result.Failure<Guid>(GenreErrors.NotFound);
            }

            IEnumerable<Actor> actors = await _actorRepository.GetByIdsAsync(request.Actors);
            if(actors.Count() != request.Actors.Count)
            {
                return Result.Failure<Guid>(ActorErrors.NotFound);
            }

            Result<PhotoUploadResult> result = await _photoAccessor.AddPhotoAsync(request.Photo);
            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            Photo moviePhoto = Photo.Create(
                new PublicId(result.Value.PublicId),
                new Url(result.Value.Url));

            await _photoRepository.AddAsync(moviePhoto);

            Title movieTitle = new Title(request.Title);
            Description movieDescription = new Description(request.Description);
            Rating movieRating = (Rating)request.Rating;
            Trailer trailer = new Trailer(request.TrailerUrl);

            Movie movie = Movie.Create(
                movieTitle,
                movieDescription,
                movieRating,
                trailer,
                movieDuration.Value,
                _dateTimeProvider.UtcNow,
                moviePhoto,
                request.ReleaseDate);

            movie.SetGenres(genres);
            movie.SetCast(actors);

            await _movieRepository.AddAsync(movie);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return movie.Id;
        }
    }
}
