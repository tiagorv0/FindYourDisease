using FindYourDisease.Users.Application.Commands.PatientCommand;
using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Application.Enum;
using FindYourDisease.Users.Application.Queries;
using FindYourDisease.Users.Application.Service;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindYourDisease.Users.API.Controllers;

public class PatientController : BaseController
{
    private readonly IMediator _mediator;
    private readonly INotificationCollector _notification;

    public PatientController(IMediator mediator, INotificationCollector notification)
    {
        _mediator = mediator;
        _notification = notification;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePatient([FromForm] PatientRequest patient)
    {
        var command = new CreatePatientCommand
        {
            PatientRequest = patient
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpPut("update")]
    [Authorize(RolePolicy.Patient)]
    public async Task<IActionResult> UpdatePatient([FromForm] PatientRequest patient)
    {
        var command = new UpdatePatientCommand
        {
            Id = UserId,
            PatientRequest = patient
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpDelete("delete")]
    [Authorize(RolePolicy.Patient)]
    public async Task<IActionResult> DeletePatient()
    {
        var command = new DeletePatientCommand
        {
            Id = UserId
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient([FromRoute] long id)
    {
        var command = new GetByIdPatientQuery
        {
            Id = id
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients([FromQuery] bool active)
    {
        var command = new GetAllPatientQuery()
        {
            Active = active
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }
}
