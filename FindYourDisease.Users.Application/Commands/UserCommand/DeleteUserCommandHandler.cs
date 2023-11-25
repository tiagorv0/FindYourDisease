using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Interfaces;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.UserCommand;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationCollector _notificationCollector;

    public DeleteUserCommandHandler(IUserRepository userRepository, INotificationCollector notificationCollector)
    {
        _userRepository = userRepository;
        _notificationCollector = notificationCollector;
    }

    public async Task<UserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

        if (user == null)
        {
            _notificationCollector.AddNotification(ErrorMessages.User_Not_Found);
            return default;
        }

        await _userRepository.DeleteAsync(user.Id, cancellationToken);

        return UserResponse.FromUser(user);
    }
}
