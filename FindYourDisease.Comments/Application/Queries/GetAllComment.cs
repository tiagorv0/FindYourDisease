using FindYourDisease.Comments.Domain.DTO;
using MediatR;

namespace FindYourDisease.Comments.Application.Queries;

public class GetAllComment : IRequest<IEnumerable<CommentResponse>>
{
    public CommentParams CommentParams { get; set; }
}
