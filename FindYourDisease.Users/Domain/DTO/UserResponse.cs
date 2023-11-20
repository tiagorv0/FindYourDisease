using FindYourDisease.Users.Domain.Model;

namespace FindYourDisease.Users.Domain.DTO;

public class UserResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Photo { get; set; }
    public DateTime BirthDate { get; set; }
    public string Localization { get; set; }

    public static UserResponse FromUser(User user)
        => new()
        {
            Id = user.Id,
            Name = user.Name,
            Description = user.Description,
            Email = user.Email,
            Phone = user.Phone,
            CreatedAt = user.CreatedAt,
            Photo = user.Photo,
            BirthDate = user.BirthDate,
            Localization = user.Localization()
        };

    public static IEnumerable<UserResponse> FromUser(IEnumerable<User> users)
        => users.Select(FromUser);
}
