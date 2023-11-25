using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Interfaces;
using FindYourDisease.Users.Domain.Model;
using MediatR;
using SecureIdentity.Password;

namespace FindYourDisease.Users.Application.Commands.UserCommand;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationCollector _notificationCollector;
    private readonly IFileStorageService _fileStorage;

    public UpdateUserCommandHandler(IUserRepository userRepository,
        IFileStorageService fileStorage,
        INotificationCollector notificationCollector)
    {
        _userRepository = userRepository;
        _notificationCollector = notificationCollector;
        _fileStorage = fileStorage;
    }

    public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await ValidationAsync(request, cancellationToken);

        if (_notificationCollector.HasNotifications())
            return default;

        if (request.UserRequest.Photo is not null)
        {
            var updatedFileName = await _fileStorage
           .UpdateFileAsync(user.Photo, request.UserRequest.Photo, Path.GetExtension(request.UserRequest.Photo.FileName));

            user.ChangePhoto(updatedFileName);
            if (_notificationCollector.HasNotifications())
                return default;
        }

        if (!string.IsNullOrEmpty(request.UserRequest.Password))
            user.ChangePassword(PasswordHasher.Hash(request.UserRequest.Password));

        user.Update(request.UserRequest.Name, request.UserRequest.Description, request.UserRequest.Occupation, request.UserRequest.Email, request.UserRequest.Phone,
            request.UserRequest.BirthDate, request.UserRequest.City, request.UserRequest.State, request.UserRequest.Country);

        await _userRepository.UpdateAsync(user, cancellationToken);

        return UserResponse.FromUser(user);
    }

    private async Task<User> ValidationAsync(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

        var userByEmail = await _userRepository.GetAsync("Email", request.UserRequest.Email, cancellationToken);

        if (user is null)
            _notificationCollector.AddNotification(ErrorMessages.User_Not_Found);

        if (userByEmail is not null && userByEmail.Id != request.Id)
            _notificationCollector.AddNotification(ErrorMessages.Email_Already_Exist);

        return user;
    }
}
