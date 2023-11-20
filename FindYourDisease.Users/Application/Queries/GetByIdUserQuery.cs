using FindYourDisease.Users.Domain.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Queries;

public class GetByIdUserQuery : IRequest<UserResponse>
{
    public long Id { get; set; }
}
