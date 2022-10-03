using Conduit.Core.Models;
using FluentValidation;

namespace Conduit.SharedKernel.Validators;

public class UserLoginValidator : AbstractValidator<UserForLoginDto>
{
    public UserLoginValidator()
    {
        RuleFor(user => user.Email).NotEmpty();
        RuleFor(user => user.Password).NotEmpty();
    }
}