using Conduit.Core.Extensions;

namespace Conduit.Core.Models;

public class ArticleDto
{
    public string Slug => Title.ToSlug();
    public string Title { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Favorited { get; set; } = false;
    public int FavoritesCount { get; set; }
}