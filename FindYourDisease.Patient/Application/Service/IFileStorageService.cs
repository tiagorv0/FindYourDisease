namespace FindYourDisease.Patient.Application.Service;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile content, string extension);
    Task<string> UpdateFileAsync(string fileName, IFormFile content, string extension);
    Task DeleteFileAsync(string fileName);
    Task<byte[]> GetFileAsync(string fileName);
}
