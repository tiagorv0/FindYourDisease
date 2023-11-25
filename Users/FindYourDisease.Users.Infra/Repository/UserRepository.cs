using FindYourDisease.Users.Domain.Interfaces;
using FindYourDisease.Users.Domain.Model;
using Microsoft.Extensions.Configuration;

namespace FindYourDisease.Users.Infra.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IConfiguration config) : base(config)
    {
    }
}
