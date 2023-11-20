﻿using FindYourDisease.Users.Application.Commands;
using FindYourDisease.Users.Application.Queries;
using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FindYourDisease.Users.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly INotificationCollector _notification;

    public UserController(IMediator mediator, INotificationCollector notificationCollector)
    {
        _mediator = mediator;
        _notification = notificationCollector;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromForm] UserRequest patient)
    {
        var command = new CreateUserCommand
        {
            UserRequest = patient
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> CreateUser([FromRoute] long id, [FromForm] UserRequest patient)
    {
        var command = new UpdateUserCommand
        {
            Id = id,
            UserRequest = patient
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] long id)
    {
        var command = new DeleteUserCommand
        {
            Id = id
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] long id)
    {
        var command = new GetByIdUserQuery
        {
            Id = id
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery] UserParams userParams)
    {
        var command = new GetAllUserQuery()
        {
            UserParams = userParams
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }
}
