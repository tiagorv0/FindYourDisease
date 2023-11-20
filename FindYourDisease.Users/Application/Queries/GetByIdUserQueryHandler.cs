using FindYourDisease.Users.Domain.DTO;
using FindYourDisease.Users.Infra.Repository;
using MediatR;

namespace FindYourDisease.Users.Application.Queries;

public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetByIdUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        return UserResponse.FromUser(user);
    }
}
