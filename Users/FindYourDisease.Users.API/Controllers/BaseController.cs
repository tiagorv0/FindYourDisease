using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindYourDisease.Users.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    protected long UserId => long.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value);
}
