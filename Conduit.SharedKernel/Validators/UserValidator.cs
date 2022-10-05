using Conduit.Core.Entities;
using FluentValidation;

namespace Conduit.SharedKernel.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.UserName).NotEmpty();
        RuleFor(user => user.Email).NotEmpty();
        RuleFor(user => user.Password).NotEmpty();
        RuleFor(user => user.Image).Custom((image, context) =>
        {
            if (!image.ToLower().Contains("image"))
                context.AddFailure("Invalid Image Url!");
        }).When(user => user.Image != default);
    }
}