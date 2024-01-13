using FindYourDisease.Users.Application.Caching;
using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.UserCommand;

public class DeleteUserCommand : CacheUser, IRequest<UserResponse>
{
    public long Id { get; set; }
}
