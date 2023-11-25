using FindYourDisease.Comments.Domain.Model;

namespace FindYourDisease.Comments.Domain.DTO;

public class CommentResponse
{
    public CommentResponse(Guid id, long reportId, long? commentReplyId, string text, string userName, bool active, DateTime createdAt)
    {
        Id = id;
        ReportId = reportId;
        CommentReplyId = commentReplyId;
        Text = text;
        UserName = userName;
        Active = active;
        CreatedAt = createdAt;
    }

    public Guid Id { get; private set; }
    public long ReportId { get; private set; }
    public long? CommentReplyId { get; private set; }
    public string Text { get; private set; }
    public string UserName { get; private set; }
    public bool Active { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static CommentResponse FromComment(Comment comment)
        => new(comment.Id, comment.ReportId, comment.CommentReplyId, comment.Text, comment.UserName, comment.Active, comment.CreatedAt);

    public static IEnumerable<CommentResponse> FromComment(IEnumerable<Comment> comments)
        => comments.Select(FromComment);
}
