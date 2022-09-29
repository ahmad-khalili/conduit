using Conduit.Core.Entities;
using Conduit.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly ConduitDbContext _context;

    public ArticleRepository(ConduitDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Article>> GetArticlesAsync(int offset, int limit)
    {
        var collection = _context.Articles as IQueryable<Article>;

        var collectionToReturn = await collection.OrderBy(a => a.CreatedAt)
            .Skip(offset).Take(limit).ToListAsync();

        return collectionToReturn;
    }
}