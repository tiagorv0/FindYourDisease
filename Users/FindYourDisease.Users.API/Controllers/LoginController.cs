using FindYourDisease.Users.Application.Commands.PatientCommand;
using FindYourDisease.Users.Application.Commands.UserCommand;
using FindYourDisease.Users.Application.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FindYourDisease.Users.API.Controllers;

public class LoginController : BaseController
{
    private readonly IMediator _mediator;
    private readonly INotificationCollector _notification;

    public LoginController(IMediator mediator, INotificationCollector notification)
    {
        _mediator = mediator;
        _notification = notification;
    }

    [HttpPost("patient")]
    public async Task<IActionResult> LoginPatient([FromBody] LoginPatientCommand login)
    {
        var response = await _mediator.Send(login);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpPost("user")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand login)
    {
        var response = await _mediator.Send(login);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }
}
