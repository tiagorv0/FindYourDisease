namespace FindYourDisease.Patient.Infra.Repository;

public interface IPatientRepository
{
    Task<IEnumerable<Domain.Model.Patient>> GetAllAsync(bool? active = true, CancellationToken cancellationToken = default);
    Task<Domain.Model.Patient> GetAsync(string property, dynamic value, CancellationToken cancellationToken = default);
    Task<Domain.Model.Patient> GetByIdAsync(long id, bool? active = true, CancellationToken cancellationToken = default);
    Task<bool> ExistAsync(string property, dynamic value, CancellationToken cancellationToken = default);
    Task<long> CreateAsync(Domain.Model.Patient patient, CancellationToken cancellationToken = default);
    Task UpdateAsync(Domain.Model.Patient patient, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
