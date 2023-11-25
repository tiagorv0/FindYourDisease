using FindYourDisease.Users.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace FindYourDisease.Users.Application.DTO;

public class PatientRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public IFormFile? Photo { get; set; }
    public DateTime BirthDate { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public static Patient ToPatient(PatientRequest patient)
        => new(patient.Name, patient.Description, patient.Email, patient.Phone, patient.BirthDate, patient.City, patient.State, patient.Country);
}
