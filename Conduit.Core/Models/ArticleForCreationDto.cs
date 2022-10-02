namespace Conduit.Core.Models;

public class ArticleForCreationDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Body { get; set; } = null!;
}