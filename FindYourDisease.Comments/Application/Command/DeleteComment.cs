using MediatR;

namespace FindYourDisease.Comments.Application.Command;

public class DeleteComment : IRequest<bool>
{
    public Guid Id { get; set; }
}
