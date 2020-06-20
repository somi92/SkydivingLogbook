using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Somi92.SkydivingLogbook.Core.Features.Users;

namespace Somi92.SkydivingLogbook.Web.Controllers
{
    // [ApiController]
    // [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("Users/{id}")]
        public async Task<IActionResult> GetUser(int id)
            => Ok(await _mediator.Send(new LoadUserData.Query { Id = id }));
    }
}
