using Conduit.Core.Entities;
using Conduit.Core.Models;
using FluentValidation;

namespace Conduit.SharedKernel.Validators;

public class RegisterUserValidator : AbstractValidator<UserForCreationDto>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.UserName).NotEmpty();
        RuleFor(user => user.Email).NotEmpty();
        RuleFor(user => user.Password).NotEmpty();
    }
}