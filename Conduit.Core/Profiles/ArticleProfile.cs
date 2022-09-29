using AutoMapper;
using Conduit.Core.Entities;
using Conduit.Core.Models;

namespace Conduit.Core.Profiles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleDto>();
    }
}