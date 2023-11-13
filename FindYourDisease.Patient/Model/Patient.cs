namespace FindYourDisease.Patient.Model;

public class Patient
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public bool Active { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string HashedPassword { get; set; }
    public string Photo { get; set; }
    public DateTime BirthDate { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public string Localization()
        => $"{City}, {State}, {Country}";
}
