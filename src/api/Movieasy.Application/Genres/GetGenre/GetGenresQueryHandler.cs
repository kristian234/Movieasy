﻿using MediatR;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Application.Genres.GetGenre
{
    internal sealed class GetGenresQueryHandler : IQueryHandler<GetGenresQuery, IEnumerable<GenreResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetGenresQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Result<IEnumerable<GenreResponse>>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
