using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.UserCommand;

public class DeleteUserCommand : IRequest<UserResponse>
{
    public long Id { get; set; }
}
