namespace FindYourDisease.Patient.Infra.Repository;

public interface IPatientRepository
{
    Task<IEnumerable<Domain.Model.Patient>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Domain.Model.Patient> GetAsync(string queryCondition, CancellationToken cancellationToken = default);
    Task<Domain.Model.Patient> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> ExistAsync(string queryCondition, CancellationToken cancellationToken = default);
    Task CreateAsync(Domain.Model.Patient patient, CancellationToken cancellationToken = default);
    Task UpdateAsync(Domain.Model.Patient patient, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
