using FindYourDisease.Users.Domain.Abstractions;

namespace FindYourDisease.Users.Domain.Interfaces;
public interface IBaseRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(bool? active = true, CancellationToken cancellationToken = default);
    Task<T> GetAsync(string property, dynamic value, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(long id, bool? active = true, CancellationToken cancellationToken = default);
    Task<bool> ExistAsync(string property, dynamic value, CancellationToken cancellationToken = default);
    Task<long> CreateAsync(T patient, CancellationToken cancellationToken = default);
    Task UpdateAsync(T patient, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
