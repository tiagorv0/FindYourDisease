using FindYourDisease.Comments.Domain.DTO;
using MediatR;

namespace FindYourDisease.Comments.Application.Command;

public class UpdateComment : IRequest<CommentResponse>
{
    public Guid Id { get; set; }
    public UpdateCommentRequest UpdateCommentRequest { get; set; }
}
