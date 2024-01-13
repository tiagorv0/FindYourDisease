using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Interfaces;
using MediatR;
using SecureIdentity.Password;

namespace FindYourDisease.Users.Application.Commands.UserCommand;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationCollector _notificationCollector;
    private readonly IFileStorageService _fileStorage;
    private readonly ICachingService _cachingService;

    public CreateUserCommandHandler(IUserRepository userRepository,
        INotificationCollector notificationCollector,
        IFileStorageService fileStorage,
        ICachingService cachingService)
    {
        _userRepository = userRepository;
        _notificationCollector = notificationCollector;
        _fileStorage = fileStorage;
        _cachingService = cachingService;
    }

    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var exist = await _userRepository.ExistAsync("Email", request.UserRequest.Email, cancellationToken);
        if (exist)
        {
            _notificationCollector.AddNotification(ErrorMessages.Email_Already_Exist);
            return default;
        }

        var user = UserRequest.ToPatient(request.UserRequest);
        user.SetPassword(PasswordHasher.Hash(request.UserRequest.Password));

        if (request.UserRequest.Photo is not null)
        {
            var fileName = await _fileStorage.SaveFileAsync(request.UserRequest.Photo, Path.GetExtension(request.UserRequest.Photo.FileName));
            user.SetPhoto(fileName);
            if (_notificationCollector.HasNotifications())
                return default;
        }

        var id = await _userRepository.CreateAsync(user, cancellationToken);
        user.Id = id;

        await _cachingService.SetAsync(request.SetCacheKey(user.Id), user);

        return UserResponse.FromUser(user);
    }
}
