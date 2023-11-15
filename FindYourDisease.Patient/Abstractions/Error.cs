namespace FindYourDisease.Patient.Abstractions;

public sealed record Error(string Code, string? Description = null)
{
    public static readonly Error None = new(string.Empty);
    public static readonly Error Patient_Not_Found = new("Patient not found");
    public static readonly Error Invalid_Extension_File = new("Invalid file extension");
    public static readonly Error File_Not_Found = new("File not found");
}
