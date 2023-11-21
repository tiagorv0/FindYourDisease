namespace FindYourDisease.Comments.Domain.Model;

public class Comment
{
    public Comment(long reportId, long? commentReplyId, string text, long userId, string userName)
    {
        Id = Guid.NewGuid();
        ReportId = reportId;
        CommentReplyId = commentReplyId;
        Text = text;
        UserId = userId;
        UserName = userName;
        CreatedAt = DateTime.UtcNow;
        Active = true;
    }

    public Guid Id { get; private set; }
    public long ReportId { get; private set; }
    public long? CommentReplyId { get; private set; }
    public string Text { get; private set; }
    public long UserId { get; private set; }
    public string UserName { get; private set; }
    public bool Active { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public void Update(string text, bool active)
    {
        Text = text;
        Active = active;
        UpdatedAt = DateTime.UtcNow;
    }
}
