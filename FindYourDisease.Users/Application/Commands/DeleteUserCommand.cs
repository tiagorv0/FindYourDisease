using FindYourDisease.Users.Domain.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands;

public class DeleteUserCommand : IRequest<UserResponse>
{
    public long Id { get; set; }
}
