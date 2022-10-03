using Conduit.Core.Models;
using FluentValidation;

namespace Conduit.SharedKernel.Validators;

public class UserRegistrationValidator : AbstractValidator<UserForCreationDto>
{
    public UserRegistrationValidator()
    {
        RuleFor(user => user.UserName).NotEmpty();
        RuleFor(user => user.Email).NotEmpty();
        RuleFor(user => user.Password).NotEmpty();
    }
}