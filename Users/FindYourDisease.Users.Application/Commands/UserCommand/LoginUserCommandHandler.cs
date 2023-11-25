using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Interfaces;
using MediatR;
using SecureIdentity.Password;

namespace FindYourDisease.Users.Application.Commands.UserCommand;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IAuthService _authService;
    private readonly INotificationCollector _notification;
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IAuthService authService, INotificationCollector notification, IUserRepository userRepository)
    {
        _authService = authService;
        _notification = notification;
        _userRepository = userRepository;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync("Email", request.Email);

        if (user is null || !PasswordHasher.Verify(user.HashedPassword, request.Password))
        {
            _notification.AddNotification(ErrorMessages.User_Not_Found);
            return default;
        }

        var token = _authService.GenerateTokenUser(user);

        return token;
    }
}
