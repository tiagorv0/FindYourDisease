using Dapper;
using FindYourDisease.Patient.Model;
using Npgsql;

namespace FindYourDisease.Patient.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly IConfiguration _config;
    private const string NAME_CONNECTION = "PatientConnection";

    public PatientRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<Model.Patient> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = "SELECT * FROM Patients WHERE Id = @id";
            return await db.QueryFirstOrDefaultAsync<Model.Patient>(new CommandDefinition(script, new { id }, cancellationToken: cancellationToken));
        }
    }

    public async Task<IEnumerable<Model.Patient>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = "SELECT * FROM Patients";
            return await db.QueryAsync<Model.Patient>(new CommandDefinition(script, cancellationToken: cancellationToken));
        }
    }

    public async Task<bool> ExistEmailAsync(string queryCondition, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = "SELECT * FROM Patients @queryCondition";
            var result = await db.QuerySingleOrDefaultAsync<Model.Patient>(new CommandDefinition(script, new { queryCondition }, cancellationToken: cancellationToken));
            return result != null;
        }
    }

    public async Task CreateAsync(Model.Patient patient, CancellationToken cancellationToken = default)
    {
        patient.CreatedAt = DateTime.Now;
        patient.UpdateAt = DateTime.Now;
        patient.Active = true;
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = @"INSERT INTO Patients(CreatedAt, UpdateAt, Name, Description, Email, Phone, HashedPassword, Photo, BirthDate, City, State, Country, Active)
                VALUES (@CreatedAt, @UpdateAt, @Name, @Description, @Email, @Phone, @HashedPassword, @Photo, @BirthDate, @City, @State, @Country, @Active)
                WHERE Email = @Email";
            await db.ExecuteAsync(new CommandDefinition(script, patient, cancellationToken: cancellationToken));
        }
    }

    public async Task UpdateAsync(Model.Patient patient, CancellationToken cancellationToken = default)
    {
        patient.UpdateAt = DateTime.Now;
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = @"Update Patients
                SET (CreatedAt = @CreatedAt, UpdateAt = @UpdateAt, Name = @Name, Description = @Description, Email = @Email, Phone = @Phone, HashedPassword = @HashedPassword, Photo = @Photo, BirthDate = @BirthDate, City = @City, State = @State, Country = @Country, Active = @Active)
                WHERE Id = @d";
            await db.QuerySingleOrDefaultAsync<Model.Patient>(new CommandDefinition(script, patient, cancellationToken: cancellationToken));
        }
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = @"Update Patients 
                SET Active = @active, 
                UpdateAt = @updateAt 
                WHERE Id = @id";
            await db.ExecuteAsync(script, new { updateAt = DateTime.Now, active = false, id });
        }
    }
}
