using FindYourDisease.Users.Application.Caching;
using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.UserCommand;

public class CreateUserCommand : CacheUser, IRequest<UserResponse>
{
    public UserRequest UserRequest { get; set; }
}
