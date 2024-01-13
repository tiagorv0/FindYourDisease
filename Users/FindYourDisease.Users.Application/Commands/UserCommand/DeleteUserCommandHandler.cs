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
    private readonly ICachingService _cachingService;

    public DeleteUserCommandHandler(IUserRepository userRepository, INotificationCollector notificationCollector, ICachingService cachingService)
    {
        _userRepository = userRepository;
        _notificationCollector = notificationCollector;
        _cachingService = cachingService;
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

        await _cachingService.RemoveAsync(request.SetCacheKey(user.Id));

        return UserResponse.FromUser(user);
    }
}
