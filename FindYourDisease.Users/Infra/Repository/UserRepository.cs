using FindYourDisease.Users.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using FindYourDisease.Users.Infra.Extensions;

namespace FindYourDisease.Users.Infra.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User> DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<bool> ExistAsync(Expression<Func<User, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(filter);
    }

    public async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> filter = null,
            int? skip = null,
            int? take = null,
            string orderBy = null,
            string includeProps = null,
            bool asNoTracking = true,
            CancellationToken cancellationToken = default)
    {
        var query = _context.Users.AsQueryable();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        if (!string.IsNullOrWhiteSpace(includeProps))
        {
            query = includeProps.Split(',', StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (q, p) => q.Include(p));
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            query = query.OrderBy(orderBy);
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue && take.Value > 0)
        {
            query = query.Take(take.Value);
        }

        _context.Database.SetCommandTimeout(TimeSpan.FromMinutes(2));

        return await query.ToListAsync();
    }

    public Task<User> GetAsync(string property, dynamic value, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.Active);
    }

    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }
}
