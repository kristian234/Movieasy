using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Application.Movies.DeleteMovie;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Application.Genres.DeleteGenre
{
    internal class DeleteGenreCommandHandler : ICommandHandler<DeleteGenreCommand>
    { 


        public DeleteGenreCommandHandler()
        {

        }

        public Task<Result> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
