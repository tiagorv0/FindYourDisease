using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Interfaces;
using FindYourDisease.Users.Domain.Model;
using MediatR;
using System.Text.Json;

namespace FindYourDisease.Users.Application.Queries;

public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationCollector _notificationCollector;
    private readonly ICachingService _cachingService;

    public GetByIdUserQueryHandler(IUserRepository userRepository, 
        INotificationCollector notificationCollector, 
        ICachingService cachingService)
    {
        _userRepository = userRepository;
        _notificationCollector = notificationCollector;
        _cachingService = cachingService;
    }

    public async Task<UserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var cache = await _cachingService.GetAsync(request.SetCacheKey(request.Id));

        if(cache is null)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

            if (user == null)
            {
                _notificationCollector.AddNotification(ErrorMessages.User_Not_Found);
                return default;
            }

            await _cachingService.SetAsync(request.SetCacheKey(user.Id), user);
            return UserResponse.FromUser(user);
        }

        var cachedUser = JsonSerializer.Deserialize<User>(cache);
        return UserResponse.FromUser(cachedUser);
    }
}
