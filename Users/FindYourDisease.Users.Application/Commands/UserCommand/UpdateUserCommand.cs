using FindYourDisease.Users.Application.Caching;
using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.UserCommand;

public class UpdateUserCommand : CacheUser, IRequest<UserResponse>
{
    public long Id { get; set; }
    public UserRequest UserRequest { get; set; }
}
