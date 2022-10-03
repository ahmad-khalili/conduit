using AutoMapper;
using Conduit.Core.Entities;
using Conduit.Core.Models;
using Conduit.SharedKernel.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Web.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UserForCreationDto> _validator;

    public UsersController(IUserRepository userRepository, IMapper mapper, IValidator<UserForCreationDto> validator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> RegisterUser(UserForCreationDto userDto)
    {
        await _validator.ValidateAndThrowAsync(userDto);

        var user = _mapper.Map<User>(userDto);

        await _userRepository.AddUserAsync(user);

        await _userRepository.SaveChangesAsync();

        return CreatedAtAction("RegisterUser", _mapper.Map<UserDto>(user));
    }
}