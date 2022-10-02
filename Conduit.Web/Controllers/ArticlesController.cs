using AutoMapper;
using Conduit.Core.Entities;
using Conduit.Core.Models;
using Conduit.SharedKernel.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Web.Controllers;

[Route("api/articles")]
[ApiController]
public class ArticlesController : Controller
{
    private const int DefaultOffset = 0;
    private const int DefaultLimit = 20;
    
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<Article> _validator;

    public ArticlesController(IArticleRepository articleRepository, IMapper mapper,
        IValidator<Article> validator)
    {
        _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    [HttpGet]
    public async Task<ActionResult> GetArticles(int offset = DefaultOffset, 
        int limit = DefaultLimit)
    {
        var articlesList = await _articleRepository.GetArticlesAsync(offset, limit);

        var articles = _mapper.Map<IEnumerable<ArticleDto>>(articlesList);

        var articlesCount = await _articleRepository.GetCountAsync();

        return Ok(new
        {
            articles,
            articlesCount
        });
    }

    [HttpPost]
    public async Task<ActionResult<ArticleDto>> CreateArticle(ArticleForCreationDto article)
    {
        var articleToCreate = _mapper.Map<Article>(article);

        await _validator.ValidateAndThrowAsync(articleToCreate);

        await _articleRepository.AddArticleAsync(articleToCreate);

        await _articleRepository.SavesChangesAsync();

        var createdArticle = _mapper.Map<Article>(articleToCreate);

        return Ok(createdArticle);
    }
}