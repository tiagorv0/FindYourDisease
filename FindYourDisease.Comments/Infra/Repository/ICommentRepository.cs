using FindYourDisease.Comments.Domain.DTO;
using FindYourDisease.Comments.Domain.Model;
using System.Linq.Expressions;

namespace FindYourDisease.Comments.Infra.Repository;

public interface ICommentRepository
{
    Task AddAsync(Comment comment, CancellationToken cancellationToken = default);
    Task<Comment> GetOneAsync(Expression<Func<Comment, bool>> filter = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<Comment>> GetAllAsync(CommentParams commentParams, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, Comment commentAtualizada, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
