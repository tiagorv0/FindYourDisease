using FindYourDisease.Users.Application.Caching;
using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Queries;

public class GetByIdUserQuery : CacheUser, IRequest<UserResponse>
{
    public long Id { get; set; }

}
