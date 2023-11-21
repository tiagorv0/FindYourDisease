using FindYourDisease.Comments.Domain.DTO;
using FindYourDisease.Comments.Domain.Model;
using FindYourDisease.Comments.Domain.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace FindYourDisease.Comments.Infra.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly IMongoCollection<Comment> _comments;

    public CommentRepository(IOptions<DatabaseOptions> options)
    {
        var databaseOptions = options.Value;
        var client = new MongoClient(databaseOptions.ConnectionString);
        var database = client.GetDatabase(databaseOptions.DatabaseName);

        _comments = database.GetCollection<Comment>("Comments");
    }

    public async Task AddAsync(Comment comment, CancellationToken cancellationToken = default)
    {
        await _comments.InsertOneAsync(comment, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, Comment comment, CancellationToken cancellationToken = default)
    {
        await _comments.ReplaceOneAsync(comment => comment.Id == id, comment, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Comment>> GetAllAsync(CommentParams commentParams, CancellationToken cancellationToken = default)
    {
        var query = _comments.AsQueryable();

        if (commentParams.Active.HasValue)
        {
            query = query.Where(x => x.Active == commentParams.Active.Value);
        }

        if (commentParams.Id.HasValue)
        {
            query = query.Where(x => x.Id == commentParams.Id);
        }

        if (commentParams.UserId.HasValue)
        {
            query = query.Where(x => x.UserId == commentParams.UserId);
        }

        if (commentParams.ReportId.HasValue)
        {
            query = query.Where(x => x.ReportId == commentParams.ReportId);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Comment> GetOneAsync(Expression<Func<Comment, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        return await _comments.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _comments.DeleteOneAsync(comment => comment.Id == id, cancellationToken);
    }
}
