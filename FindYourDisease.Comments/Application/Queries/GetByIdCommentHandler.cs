using FindYourDisease.Comments.Application.Service;
using FindYourDisease.Comments.Domain.Abstractions;
using FindYourDisease.Comments.Domain.DTO;
using FindYourDisease.Comments.Infra.Repository;
using MediatR;

namespace FindYourDisease.Comments.Application.Queries;

public class GetByIdCommentHandler : IRequestHandler<GetByIdComment, CommentResponse>
{
    private readonly ICommentRepository _commentRepository;
    private readonly INotificationCollector _notificationCollector;

    public GetByIdCommentHandler(ICommentRepository commentRepository, INotificationCollector notificationCollector)
    {
        _commentRepository = commentRepository;
        _notificationCollector = notificationCollector;
    }

    public async Task<CommentResponse> Handle(GetByIdComment request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetOneAsync(x => x.Id == request.Id, cancellationToken);

        if (comment == null)
        {
            _notificationCollector.AddNotification(ErrorMessages.Comment_Not_Found);
            return default;
        }

        return CommentResponse.FromComment(comment);
    }
}
