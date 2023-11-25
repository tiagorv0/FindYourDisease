using MediatR;

namespace FindYourDisease.Users.Application.Commands.UserCommand;
public class LoginUserCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
