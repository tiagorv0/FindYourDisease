using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Queries;

public class GetAllUserQuery : IRequest<List<UserResponse>>
{
    public UserParams UserParams { get; set; }
}
