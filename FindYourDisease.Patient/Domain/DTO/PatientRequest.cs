namespace FindYourDisease.Patient.Domain.DTO;

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

    public static Model.Patient ToPatient(PatientRequest patient)
        => new()
        {
            Name = patient.Name,
            Description = patient.Description,
            Email = patient.Email,
            Phone = patient.Phone,
            BirthDate = patient.BirthDate,
            City = patient.City,
            State = patient.State,
            Country = patient.Country
        };
}
