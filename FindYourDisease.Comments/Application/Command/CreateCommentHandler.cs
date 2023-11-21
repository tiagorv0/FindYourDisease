using FindYourDisease.Comments.Application.Service;
using FindYourDisease.Comments.Domain.DTO;
using FindYourDisease.Comments.Infra.Repository;
using MediatR;
using System.Reflection.Metadata;

namespace FindYourDisease.Comments.Application.Command;

public class CreateCommentHandler : IRequestHandler<CreateComment, CommentResponse>
{
    private readonly ICommentRepository _commentRepository;
    private readonly INotificationCollector _notificationCollector;

    public CreateCommentHandler(ICommentRepository commentRepository, INotificationCollector notificationCollector)
    {
        _commentRepository = commentRepository;
        _notificationCollector = notificationCollector;
    }

    public async Task<CommentResponse> Handle(CreateComment request, CancellationToken cancellationToken)
    {
        var comment = CommentRequest.ToComment(request.CommentRequest);

        await _commentRepository.AddAsync(comment, cancellationToken);

        return CommentResponse.FromComment(comment);
    }
}
