﻿using FindYourDisease.Users.Domain.Model;

namespace FindYourDisease.Users.Application.DTO;

public class PatientResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Photo { get; set; }
    public DateTime BirthDate { get; set; }
    public string Localization { get; set; }

    public static PatientResponse FromPatient(Patient patient)
        => new()
        {
            Id = patient.Id,
            Name = patient.Name,
            Description = patient.Description,
            Email = patient.Email,
            Phone = patient.Phone,
            CreatedAt = patient.CreatedAt,
            Photo = patient.Photo,
            BirthDate = patient.BirthDate,
            Localization = patient.Localization()
        };

    public static IEnumerable<PatientResponse> FromPatient(IEnumerable<Patient> patients)
        => patients.Select(FromPatient);
}
