using FindYourDisease.Comments.Domain.Model;

namespace FindYourDisease.Comments.Domain.DTO;

public class CommentResponse
{
    public Guid Id { get; set; }
    public long ReportId { get; set; }
    public long? CommentReplyId { get; set; }
    public string Text { get; set; }
    public string UserName { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }

    public static CommentResponse FromComment(Comment comment)
        => new()
        {
            Id = comment.Id,
            ReportId = comment.ReportId,
            CommentReplyId = comment.CommentReplyId,
            Text = comment.Text,
            UserName = comment.UserName,
            Active = comment.Active,
            CreatedAt = comment.CreatedAt
        };

    public static IEnumerable<CommentResponse> FromComment(IEnumerable<Comment> comments)
        => comments.Select(FromComment);
}
