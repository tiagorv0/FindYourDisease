using FindYourDisease.Users.Application.Login;
using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FindYourDisease.Users.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly INotificationCollector _notification;

    public LoginController(ILoginService loginService, INotificationCollector notification)
    {
        _loginService = loginService;
        _notification = notification;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        var response = await _loginService.LoginAsync(login);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }
}
