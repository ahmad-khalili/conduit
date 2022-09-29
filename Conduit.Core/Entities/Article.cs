using System.ComponentModel.DataAnnotations.Schema;

namespace Conduit.Core.Entities;

public class Article
{
    public int ArticleId { get; set; }

    [NotMapped]
    public string Slug => Title.Replace("%20", "-");
    public string Title { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [NotMapped]
    public bool Favorited { get; set; }
    public int FavoritesCount { get; set; }
}