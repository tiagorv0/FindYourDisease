using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.DTO;
using FindYourDisease.Users.Infra.Repository;
using MediatR;
using SecureIdentity.Password;

namespace FindYourDisease.Users.Application.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationCollector _notificationCollector;
    private readonly IFileStorageService _fileStorage;

    public CreateUserCommandHandler(IUserRepository userRepository, 
        INotificationCollector notificationCollector, 
        IFileStorageService fileStorage)
    {
        _userRepository = userRepository;
        _notificationCollector = notificationCollector;
        _fileStorage = fileStorage;
    }

    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var exist = await _userRepository.ExistAsync(x => x.Email == request.UserRequest.Email, cancellationToken);
        if (exist)
        {
            _notificationCollector.AddNotification(ErrorMessages.Email_Already_Exist);
            return default;
        }

        string fileName = null;
        if (request.UserRequest.Photo is not null)
        {
            fileName = await _fileStorage.SaveFileAsync(request.UserRequest.Photo, Path.GetExtension(request.UserRequest.Photo.FileName));
            if (_notificationCollector.HasNotifications())
                return default;
        }

        var user = UserRequest.ToPatient(request.UserRequest);
        user.Photo = fileName;

        user.HashedPassword = PasswordHasher.Hash(request.UserRequest.Password);

        var newUser = await _userRepository.CreateAsync(user, cancellationToken);

        return UserResponse.FromUser(newUser);
    }
}
