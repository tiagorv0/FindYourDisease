using FindYourDisease.Users.Domain.Abstractions;

namespace FindYourDisease.Users.Domain.Model;

public class Patient : BaseEntity
{
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

    public Patient()
    {
        
    }

    public Patient(string name, string description, string email, string phone, 
         DateTime birthDate, string city, string state, string country)
    {
        Name = name;
        Description = description;
        Email = email;
        Phone = phone;
        BirthDate = birthDate;
        City = city;
        State = state;
        Country = country;
        Active = true;
        CreatedAt = DateTime.Now;
    }

    public void Update(string name, string description, string email, string phone, 
                DateTime birthDate, string city, string state, string country)
    {
        Name = name;
        Description = description;
        Email = email;
        Phone = phone;
        BirthDate = birthDate;
        City = city;
        State = state;
        Country = country;
        DoUpdateAt();
    }

    public void SetPassword(string hashedPassword)
        => HashedPassword = hashedPassword;

    public void ChangePassword(string hashedPassword)
    {
        HashedPassword = hashedPassword;
        DoUpdateAt();
    }

    public void SetPhoto(string photo)
        => Photo = photo;

    public void ChangePhoto(string photo)
    {
        Photo = photo;
        DoUpdateAt();
    }

    public string Localization()
        => $"{City}, {State}, {Country}";

    private void DoUpdateAt()
        => UpdateAt = DateTime.Now;
}
