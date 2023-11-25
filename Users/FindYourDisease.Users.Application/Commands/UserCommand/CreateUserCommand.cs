using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.UserCommand;

public class CreateUserCommand : IRequest<UserResponse>
{
    public UserRequest UserRequest { get; set; }
}
