﻿using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FindYourDisease.Users.Application.Service;

public class FileStorageService : IFileStorageService
{
    private readonly PathOptions _pathOptions;
    private readonly INotificationCollector _notificationCollector;
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };

    public FileStorageService(IOptions<PathOptions> options, INotificationCollector notificationCollector)
    {
        _pathOptions = options.Value;
        _notificationCollector = notificationCollector;
    }

    public async Task<string> SaveFileAsync(IFormFile content, string extension)
    {
        if (!_allowedExtensions.Contains(extension.ToLower()))
        {
            _notificationCollector.AddNotification(ErrorMessages.Invalid_Extension_File);
            return default;
        }

        var fileName = await SaveFile(content, extension, HandleFileName(null));

        return fileName;
    }

    public async Task<string> UpdateFileAsync(string? fileName, IFormFile content, string extension)
    {
        if (!_allowedExtensions.Contains(extension.ToLower()))
        {
            _notificationCollector.AddNotification(ErrorMessages.Invalid_Extension_File);
            return default;
        }

        var actualExtensionFile = Path.GetExtension(fileName);

        var updateFileName = await SaveFile(content, extension, HandleFileName(fileName));

        return updateFileName;
    }

    public async Task DeleteFileAsync(string fileName)
    {
        var path = Path.Combine(_pathOptions.Path, fileName);

        if (File.Exists(path))
            await Task.Run(() => File.Delete(path));
        else
            _notificationCollector.AddNotification(ErrorMessages.File_Not_Found);
    }

    public async Task<byte[]> GetFileAsync(string fileName)
    {
        var path = Path.Combine(_pathOptions.Path, fileName);

        if (!File.Exists(path))
        {
            _notificationCollector.AddNotification(ErrorMessages.File_Not_Found);
            return default;
        }

        return await File.ReadAllBytesAsync(path);
    }

    private async Task<string> SaveFile(IFormFile content, string extension, string fileName)
    {
        using (var ms = new MemoryStream())
        {
            content.CopyTo(ms);
            var fileBytes = ms.ToArray();
            fileName = $"{fileName}{extension}";
            var path = Path.Combine(_pathOptions.Path, fileName);

            await File.WriteAllBytesAsync(path, fileBytes);

            return fileName;
        }
    }

    private string HandleFileName(string? fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return Guid.NewGuid().ToString();

        var actualExtensionFile = Path.GetExtension(fileName);
        return fileName.Replace(actualExtensionFile, "");
    }
}
