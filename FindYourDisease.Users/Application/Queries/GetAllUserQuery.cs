using FindYourDisease.Users.Domain.DTO;
using FindYourDisease.Users.Infra.Repository;
using MediatR;

namespace FindYourDisease.Users.Application.Queries;

public class GetAllUserQuery : IRequest<List<UserResponse>>
{
    public UserParams UserParams { get; set; }
}
