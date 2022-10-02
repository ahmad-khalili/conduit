using Conduit.Core.Entities;

namespace Conduit.SharedKernel.Interfaces;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetArticlesAsync(int offset, int limit);
    Task AddArticleAsync(Article article);

    Task<int> GetCountAsync();
    
    Task SavesChangesAsync();
}