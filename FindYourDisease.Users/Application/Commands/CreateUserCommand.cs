using FindYourDisease.Users.Domain.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands;

public class CreateUserCommand : IRequest<UserResponse>
{
    public UserRequest UserRequest { get; set; }
}
