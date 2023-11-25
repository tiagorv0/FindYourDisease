using Dapper;
using FindYourDisease.Users.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace FindYourDisease.Users.Infra.Repository;
public abstract class BaseRepository<T> where T : BaseEntity
{
    protected readonly IConfiguration _config;
    protected const string NAME_CONNECTION = "UserConnection";

    protected BaseRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<T> GetByIdAsync(long id, bool? active = true, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = $"SELECT * FROM \"{typeof(T).Name}\" WHERE \"Id\" = @Id AND \"Active\" = Cast(@Active as integer) OR @Active IS NULL";
            return await db.QueryFirstOrDefaultAsync<T>(new CommandDefinition(script, new { Id = id, Active = active }, cancellationToken: cancellationToken));
        }
    }

    public async Task<T> GetAsync(string property, dynamic value, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = $"SELECT * FROM \"{typeof(T).Name}\" WHERE \"{property}\" = @Value";
            return await db.QueryFirstOrDefaultAsync<T>(new CommandDefinition(script, new { Value = value }, cancellationToken: cancellationToken));
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync(bool? active = true, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = $"SELECT * FROM \"{typeof(T).Name}\" WHERE \"Active\" = Cast(@Active as integer) OR @Active IS NULL";
            return await db.QueryAsync<T>(new CommandDefinition(script, new { Active = active }, cancellationToken: cancellationToken));
        }
    }

    public async Task<bool> ExistAsync(string property, dynamic value, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = $"SELECT * FROM \"{typeof(T).Name}\" WHERE \"{property}\" = @Value";
            var result = await db.QuerySingleOrDefaultAsync<int?>(new CommandDefinition(script, new { Value = value }, cancellationToken: cancellationToken));
            return result.HasValue;
        }
    }

    public async Task<long> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = $"INSERT INTO \"{typeof(T).Name}\" (\"CreatedAt\", \"UpdateAt\", \"Name\", \"Description\", \"Email\", \"Phone\", \"HashedPassword\", \"Photo\", \"BirthDate\", \"City\", \"State\", \"Country\", \"Active\")" +
                "VALUES (@CreatedAt, @UpdateAt, @Name, @Description, @Email, @Phone, @HashedPassword, @Photo, @BirthDate, @City, @State, @Country, Cast(@Active as integer))" +
                "RETURNING \"Id\";";
            var patientId = await db.ExecuteScalarAsync<long>(new CommandDefinition(script, entity, cancellationToken: cancellationToken));

            return patientId;
        }
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = $"Update \"{typeof(T).Name}\"" +
                "SET \"CreatedAt\" = @CreatedAt, \"UpdateAt\" = @UpdateAt, \"Name\" = @Name, \"Description\" = @Description, \"Email\" = @Email, \"Phone\" = @Phone, \"HashedPassword\" = @HashedPassword, \"Photo\" = @Photo, \"BirthDate\" = @BirthDate, \"City\" = @City, \"State\" = @State, \"Country\" = @Country, \"Active\" = Cast(@Active as integer)" +
                "WHERE \"Id\" = @Id";
            await db.ExecuteAsync(new CommandDefinition(script, entity, cancellationToken: cancellationToken));
        }
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        using (var db = new NpgsqlConnection(_config.GetConnectionString(NAME_CONNECTION)))
        {
            await db.OpenAsync();
            var script = $"Update \"{typeof(T).Name}\" SET \"Active\" = Cast(@Active as integer), \"UpdateAt\" = @UpdateAt WHERE \"Id\" = @Id";
            await db.ExecuteAsync(script, new { UpdateAt = DateTime.Now, Active = false, Id = id });
        }
    }
}
