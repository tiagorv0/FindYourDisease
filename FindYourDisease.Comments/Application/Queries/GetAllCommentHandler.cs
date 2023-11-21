using FindYourDisease.Comments.Domain.DTO;
using FindYourDisease.Comments.Infra.Repository;
using MediatR;

namespace FindYourDisease.Comments.Application.Queries;

public class GetAllCommentHandler : IRequestHandler<GetAllComment, IEnumerable<CommentResponse>>
{
    private readonly ICommentRepository _commentRepository;

    public GetAllCommentHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentResponse>> Handle(GetAllComment request, CancellationToken cancellationToken)
    {
        var result = await _commentRepository.GetAllAsync(request.CommentParams, cancellationToken);

        return CommentResponse.FromComment(result);
    }
}
