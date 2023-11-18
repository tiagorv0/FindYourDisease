using Dapper;
using Npgsql;

namespace FindYourDisease.Patient.Infra.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly IConfiguration _config;
    private const string NAME_CONNECTION = "PatientConnection";

    public PatientRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<Domain.Model.Patient> GetByIdAsync(long id, bool? active = true, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = "SELECT * FROM \"Patients\" WHERE \"Id\" = @Id AND \"Active\" = Cast(@Active as integer) OR @Active IS NULL";
            return await db.QueryFirstOrDefaultAsync<Domain.Model.Patient>(new CommandDefinition(script, new { Id = id, Active = active }, cancellationToken: cancellationToken));
        }
    }

    public async Task<Domain.Model.Patient> GetAsync(string property, dynamic value, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = $"SELECT * FROM \"Patients\" WHERE \"{property}\" = @Value";
            return await db.QueryFirstOrDefaultAsync<Domain.Model.Patient>(new CommandDefinition(script, new { Value = value }, cancellationToken: cancellationToken));
        }
    }

    public async Task<IEnumerable<Domain.Model.Patient>> GetAllAsync(bool? active = true, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = "SELECT * FROM \"Patients\" WHERE \"Active\" = Cast(@Active as integer) OR @Active IS NULL";
            return await db.QueryAsync<Domain.Model.Patient>(new CommandDefinition(script, new { Active = active }, cancellationToken: cancellationToken));
        }
    }

    public async Task<bool> ExistAsync(string property, dynamic value, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = $"SELECT * FROM \"Patients\" WHERE \"{property}\" = @Value";
            var result = await db.QuerySingleOrDefaultAsync<int?>(new CommandDefinition(script, new { Value = value }, cancellationToken: cancellationToken));
            return result.HasValue;
        }
    }

    public async Task<long> CreateAsync(Domain.Model.Patient patient, CancellationToken cancellationToken = default)
    {
        patient.CreatedAt = DateTime.Now;
        patient.UpdateAt = DateTime.Now;
        patient.Active = true;
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = "INSERT INTO \"Patients\" (\"CreatedAt\", \"UpdateAt\", \"Name\", \"Description\", \"Email\", \"Phone\", \"HashedPassword\", \"Photo\", \"BirthDate\", \"City\", \"State\", \"Country\", \"Active\")" +
                "VALUES (@CreatedAt, @UpdateAt, @Name, @Description, @Email, @Phone, @HashedPassword, @Photo, @BirthDate, @City, @State, @Country, Cast(@Active as integer))" +
                "RETURNING \"Id\";";
            var patientId = await db.ExecuteScalarAsync<long>(new CommandDefinition(script, patient, cancellationToken: cancellationToken));

            return patientId;
        }
    }

    public async Task UpdateAsync(Domain.Model.Patient patient, CancellationToken cancellationToken = default)
    {
        patient.UpdateAt = DateTime.Now;
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = "Update \"Patients\"" +
                "SET \"CreatedAt\" = @CreatedAt, \"UpdateAt\" = @UpdateAt, \"Name\" = @Name, \"Description\" = @Description, \"Email\" = @Email, \"Phone\" = @Phone, \"HashedPassword\" = @HashedPassword, \"Photo\" = @Photo, \"BirthDate\" = @BirthDate, \"City\" = @City, \"State\" = @State, \"Country\" = @Country, \"Active\" = Cast(@Active as integer)" +
                "WHERE \"Id\" = @Id";
            await db.ExecuteAsync(new CommandDefinition(script, patient, cancellationToken: cancellationToken));
        }
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = "Update \"Patients\" SET \"Active\" = Cast(@Active as integer), \"UpdateAt\" = @UpdateAt WHERE \"Id\" = @Id";
            await db.ExecuteAsync(script, new { UpdateAt = DateTime.Now, Active = false, Id = id });
        }
    }

}
