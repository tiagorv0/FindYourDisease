namespace FindYourDisease.Users.Domain.Abstractions;
public abstract class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public bool Active { get; set; }
}
