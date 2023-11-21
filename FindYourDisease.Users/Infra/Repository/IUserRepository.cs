using FindYourDisease.Users.Domain.Model;
using System.Linq.Expressions;

namespace FindYourDisease.Users.Infra.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> filter = null,
            int? skip = null,
            int? take = null,
            string orderBy = null,
            string includeProps = null,
            bool asNoTracking = true, 
            CancellationToken cancellationToken = default);
    Task<User> GetAsync(Expression<Func<User, bool>> filter = null, CancellationToken cancellationToken = default);
    Task<User> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> ExistAsync(Expression<Func<User, bool>> filter = null, CancellationToken cancellationToken = default);
    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task<User> DeleteAsync(User user, CancellationToken cancellationToken = default);
}
