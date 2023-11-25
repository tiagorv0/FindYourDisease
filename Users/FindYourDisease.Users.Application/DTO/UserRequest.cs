using FindYourDisease.Users.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace FindYourDisease.Users.Application.DTO;

public class UserRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Occupation { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Password { get; set; }
    public IFormFile? Photo { get; set; }
    public DateTime BirthDate { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public bool Active { get; set; }

    public static User ToPatient(UserRequest user)
        => new(user.Name, user.Description, user.Occupation, user.Email, user.Phone, user.BirthDate, user.City, user.State, user.Country);
}
