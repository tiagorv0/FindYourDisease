using FindYourDisease.Users.Domain.DTO;
using FindYourDisease.Users.Infra.Repository;
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
        var users = await _userRepository.GetAllAsync(x => 
            x.Name.Contains(request.UserParams.Search) || x.Email.Contains(request.UserParams.Search), 
            request.UserParams.Skip, 
            request.UserParams.Take, 
            request.UserParams.OrderBy, 
            null, 
            false);

        return UserResponse.FromUser(users);
    }
}
