namespace FindYourDisease.Patient.Repository;

public interface IPatientRepository
{
    Task<IEnumerable<Model.Patient>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Model.Patient> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> ExistEmailAsync(string queryCondition, CancellationToken cancellationToken = default);
    Task CreateAsync(Model.Patient patient, CancellationToken cancellationToken = default);
    Task UpdateAsync(Model.Patient patient, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
