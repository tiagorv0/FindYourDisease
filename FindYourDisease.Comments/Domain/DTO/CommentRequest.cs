using FindYourDisease.Comments.Domain.Model;

namespace FindYourDisease.Comments.Domain.DTO;

public class CommentRequest
{
    public long ReportId { get; set; }
    public long? CommentReplyId { get; set; }
    public string Text { get; set; }
    public long UserId { get; set; }
    public string UserName { get; set; }

    public static Comment ToComment(CommentRequest request)
        => new(request.ReportId, request.CommentReplyId, request.Text, request.UserId, request.UserName);
}
