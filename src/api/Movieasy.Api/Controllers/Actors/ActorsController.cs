using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Movieasy.Api.Controllers.Actors
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ISender _sender;
        public ActorsController(ISender sender)
        {
            _sender = sender;
        }
    }
}
