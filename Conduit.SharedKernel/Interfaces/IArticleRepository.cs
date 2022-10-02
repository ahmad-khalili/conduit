﻿using Conduit.Core.Entities;

namespace Conduit.SharedKernel.Interfaces;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetArticlesAsync(int offset, int limit);

    Task<Article?> GetArticleAsync(string articleSlug);
    
    Task AddArticleAsync(Article article);

    Task<int> GetCountAsync();

    void RemoveArticle(Article article);
    
    Task SavesChangesAsync();
}