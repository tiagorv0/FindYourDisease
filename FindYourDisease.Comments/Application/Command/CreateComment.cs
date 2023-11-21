using FindYourDisease.Comments.Domain.DTO;
using MediatR;

namespace FindYourDisease.Comments.Application.Command;

public class CreateComment : IRequest<CommentResponse>
{
    public CommentRequest CommentRequest { get; set; }
}
