using Microsoft.VisualStudio.TestPlatform.TestHost;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Infrastructure;
using System.Reflection;

namespace Movieasy.ArchitectureTests.Infrastructure
{
    public abstract class BaseTest
    {
        protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;

        protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;

        protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;

        protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
    }
}
