using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Domain.Interfaces;
using MediatR;

namespace FindYourDisease.Users.Application.Queries;

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserResponse>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        return UserResponse.FromUser(users);
    }
}
