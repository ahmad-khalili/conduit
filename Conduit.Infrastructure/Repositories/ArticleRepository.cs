using Conduit.Core.Entities;
using Conduit.SharedKernel.Interfaces;
using FluentValidation;
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

    public async Task AddArticleAsync(Article article)
    {
        await _context.Articles.AddAsync(article);
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Articles.CountAsync();
    }

    public async Task SavesChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}