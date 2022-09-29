using AutoMapper;
using Conduit.Core.Models;
using Conduit.SharedKernel.Interfaces;
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

    public ArticlesController(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticles(int offset = DefaultOffset, 
        int limit = DefaultLimit)
    {
        var articles = await _articleRepository.GetArticlesAsync(offset, limit);

        return Ok(_mapper.Map<IEnumerable<ArticleDto>>(articles));
    }
}