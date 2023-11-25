using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.UserCommand;

public class UpdateUserCommand : IRequest<UserResponse>
{
    public long Id { get; set; }
    public UserRequest UserRequest { get; set; }
}
