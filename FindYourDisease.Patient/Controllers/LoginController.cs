using FindYourDisease.Patient.Application.Login;
using FindYourDisease.Patient.DTO;
using FindYourDisease.Patient.Service;
using Microsoft.AspNetCore.Mvc;

namespace FindYourDisease.Patient.Controllers;
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
