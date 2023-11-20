using FindYourDisease.Users.Domain.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands;

public class UpdateUserCommand : IRequest<UserResponse>
{
    public long Id { get; set; }
    public UserRequest UserRequest { get; set; }
}
