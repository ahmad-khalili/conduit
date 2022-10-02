using Conduit.Core.Entities;
using FluentValidation;

namespace Conduit.SharedKernel.Validators;

public class ArticleValidator : AbstractValidator<Article>
{
    public ArticleValidator()
    {
        RuleFor(article => article.Title).NotEmpty();
        RuleFor(article => article.Description).NotEmpty();
        RuleFor(article => article.Body).NotEmpty();
    }
}