using FindYourDisease.Users.Domain.Model;

namespace FindYourDisease.Users.Domain.DTO;

public class UserRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Password { get; set; }
    public IFormFile? Photo { get; set; }
    public DateTime BirthDate { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public bool Active { get; set; }

    public static User ToPatient(UserRequest patient)
        => new()
        {
            Name = patient.Name,
            Description = patient.Description,
            Email = patient.Email,
            Phone = patient.Phone,
            BirthDate = patient.BirthDate,
            City = patient.City,
            State = patient.State,
            Country = patient.Country,
            Active = patient.Active
        };
}
