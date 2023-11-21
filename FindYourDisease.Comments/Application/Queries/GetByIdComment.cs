using FindYourDisease.Comments.Domain.DTO;
using MediatR;

namespace FindYourDisease.Comments.Application.Queries;

public class GetByIdComment : IRequest<CommentResponse>
{
    public Guid Id { get; set; }
}
