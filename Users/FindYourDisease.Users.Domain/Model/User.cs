using FindYourDisease.Users.Domain.Abstractions;

namespace FindYourDisease.Users.Domain.Model;

public class User : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Occupation { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string HashedPassword { get; private set; }
    public string Photo { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }

    public User()
    {
        
    }

    public User(string name, string description, string occupation, string email, string phone, 
         DateTime birthDate, string city, string state, string country)
    {
        Name = name;
        Description = description;
        Occupation = occupation;
        Email = email;
        Phone = phone;
        BirthDate = birthDate;
        City = city;
        State = state;
        Country = country;
        Active = true;
        CreatedAt = DateTime.Now;
    }

    public void Update(string name, string description, string occupation, string email, string phone, 
                DateTime birthDate, string city, string state, string country)
    {
        Name = name;
        Description = description;
        Occupation = occupation;
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
