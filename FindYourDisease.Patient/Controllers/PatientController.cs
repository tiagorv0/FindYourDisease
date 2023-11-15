using FindYourDisease.Patient.Application.Commands;
using FindYourDisease.Patient.Application.Queries;
using FindYourDisease.Patient.DTO;
using FindYourDisease.Patient.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FindYourDisease.Patient.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
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

    [HttpPut("update/{id}")]
    public async Task<IActionResult> CreatePatient([FromRoute] long id, [FromForm] PatientRequest patient)
    {
        var command = new UpdatePatientCommand
        {
            Id = id,
            PatientRequest = patient
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeletePatient([FromRoute] long id)
    {
        var command = new DeletePatientCommand
        {
            Id = id
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
    public async Task<IActionResult> GetPatients()
    {
        var command = new GetAllPatientQuery();

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }
}
