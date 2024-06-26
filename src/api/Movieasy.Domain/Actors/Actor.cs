﻿using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Actors
{
    public sealed class Actor : Entity
    {
        private Actor() { }

        private Actor(
            Guid id,
            Name name,
            Biography biography
            )
            : base(id)
        {
            Name = name;
            Biography = biography;
        }

        public Name Name { get; private set; }
        public Biography Biography { get; private set; }

        public static Actor Create(
            Name name,
            Biography biography)
        {
            Actor actor = new Actor(
                Guid.NewGuid(),
                name,
                biography);

            return actor;
        }

        public Result Update(string name, string biography)
        {
            Name newName = new Name(name);
            Biography newBiography = new Biography(biography);

            Name = newName;
            Biography = newBiography;

            return Result.Success();
        }
    }
}
