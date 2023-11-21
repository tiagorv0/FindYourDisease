using FindYourDisease.Comments.Application.Command;
using FindYourDisease.Comments.Application.Queries;
using FindYourDisease.Comments.Application.Service;
using FindYourDisease.Comments.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FindYourDisease.Comments.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly INotificationCollector _notification;

    public CommentController(IMediator mediator, INotificationCollector notification)
    {
        _mediator = mediator;
        _notification = notification;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromForm] CommentRequest comment)
    {
        var command = new CreateComment
        {
            CommentRequest = comment
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromRoute] UpdateCommentRequest comment)
    {
        var command = new UpdateComment
        {
            Id = id,
            UpdateCommentRequest = comment
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteComment
        {
            Id = id
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var command = new GetByIdComment
        {
            Id = id
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] CommentParams commentParams)
    {
        var command = new GetAllComment()
        {
            CommentParams = commentParams
        };

        var response = await _mediator.Send(command);

        return !_notification.HasNotifications() ? Ok(response) : BadRequest(_notification.GetNotifications());
    }
}
