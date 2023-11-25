using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Interfaces;
using MediatR;

namespace FindYourDisease.Users.Application.Queries;

public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationCollector _notificationCollector;

    public GetByIdUserQueryHandler(IUserRepository userRepository, INotificationCollector notificationCollector)
    {
        _userRepository = userRepository;
        _notificationCollector = notificationCollector;
    }

    public async Task<UserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

        if (user is null)
        {
            _notificationCollector.AddNotification(ErrorMessages.User_Not_Found);
            return default;
        }

        return UserResponse.FromUser(user);
    }
}
