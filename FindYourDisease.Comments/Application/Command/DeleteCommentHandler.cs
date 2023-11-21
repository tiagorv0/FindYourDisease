using FindYourDisease.Comments.Application.Service;
using FindYourDisease.Comments.Domain.Abstractions;
using FindYourDisease.Comments.Infra.Repository;
using MediatR;

namespace FindYourDisease.Comments.Application.Command;

public class DeleteCommentHandler : IRequestHandler<DeleteComment, bool>
{
    private readonly ICommentRepository _commentRepository;
    private readonly INotificationCollector _notificationCollector;

    public DeleteCommentHandler(ICommentRepository commentRepository, INotificationCollector notificationCollector)
    {
        _commentRepository = commentRepository;
        _notificationCollector = notificationCollector;
    }

    public async Task<bool> Handle(DeleteComment request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetOneAsync(x => x.Id == request.Id, cancellationToken);

        if (comment == null)
        {
            _notificationCollector.AddNotification(ErrorMessages.Comment_Not_Found);
            return default;
        }

        await _commentRepository.DeleteAsync(request.Id, cancellationToken);

        return true;
    }
}