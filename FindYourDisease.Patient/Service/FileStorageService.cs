using FindYourDisease.Patient.Options;
using Microsoft.Extensions.Options;

namespace FindYourDisease.Patient.Service;

public class FileStorageService : IFileStorageService
{
    private readonly PathOptions _pathOptions;

    public FileStorageService(IOptions<PathOptions> options)
    {
        _pathOptions = options.Value;
    }
}
