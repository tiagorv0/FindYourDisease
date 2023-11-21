namespace FindYourDisease.Comments.Domain.DTO;

public class CommentParams
{
    public Guid? Id { get; set; }
    public long? UserId { get; set; }
    public long? ReportId { get; set; }
    public bool? Active { get; set; }
}
