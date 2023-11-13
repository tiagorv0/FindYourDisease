namespace FindYourDisease.Patient.Abstractions;

public sealed record Error(string Code, string? Description = null)
{
    public static readonly Error None = new(string.Empty);
    public static readonly Error Patient_Not_Found = new("Patient not found");
}
