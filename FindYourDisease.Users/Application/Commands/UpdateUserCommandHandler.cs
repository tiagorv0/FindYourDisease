using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.DTO;
using FindYourDisease.Users.Domain.Model;
using FindYourDisease.Users.Infra.Repository;
using MediatR;
using SecureIdentity.Password;

namespace FindYourDisease.Users.Application.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationCollector _notificationCollector;
    private readonly IFileStorageService _fileStorage;

    public UpdateUserCommandHandler(IUserRepository userRepository, 
        INotificationCollector notificationCollector, 
        IFileStorageService fileStorage)
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

        string? updatedFileName = null;
        if (request.UserRequest.Photo is not null)
        {
            updatedFileName = await _fileStorage
           .UpdateFileAsync(user.Photo, request.UserRequest.Photo, Path.GetExtension(request.UserRequest.Photo.FileName));

            if (_notificationCollector.HasNotifications())
                return default;
        }

        if (!string.IsNullOrEmpty(request.UserRequest.Password))
            user.HashedPassword = PasswordHasher.Hash(request.UserRequest.Password);

        user.Name = request.UserRequest.Name;
        user.Description = request.UserRequest.Description;
        user.Email = request.UserRequest.Email;
        user.Phone = request.UserRequest.Phone;
        user.Photo = updatedFileName ?? user.Photo;
        user.BirthDate = request.UserRequest.BirthDate;
        user.City = request.UserRequest.City;
        user.State = request.UserRequest.State;
        user.Country = request.UserRequest.Country;
        user.Active = request.UserRequest.Active;

        await _userRepository.UpdateAsync(user, cancellationToken);

        return UserResponse.FromUser(user);
    }

    private async Task<User> ValidationAsync(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        var userByEmail = await _userRepository.GetAsync(x => x.Email == request.UserRequest.Email, cancellationToken);

        if (user is null)
            _notificationCollector.AddNotification(ErrorMessages.User_Not_Found);

        if (userByEmail is not null && userByEmail.Id != request.Id)
            _notificationCollector.AddNotification(ErrorMessages.Email_Already_Exist);

        return user;
    }
}
