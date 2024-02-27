using MediatR;
using Microsoft.Extensions.Logging;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;

namespace Movieasy.Application.Abstractions.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            string name = request.GetType().Name;

            try
            {
                _logger.LogInformation("Executing command {0}", name);

                TResponse? res = await next();

                _logger.LogInformation("Command {0} processed successfully", name);

                return res;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Processing command {0} failed", name);

                throw;
            }
        }
    }
}
