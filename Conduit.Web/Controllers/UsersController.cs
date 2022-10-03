using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Conduit.Core.Entities;
using Conduit.Core.Models;
using Conduit.SharedKernel.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Conduit.Web.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UserForCreationDto> _registrationValidator;
    private readonly IValidator<UserForLoginDto> _loginValidator;
    private readonly IConfiguration _config;

    public UsersController(IUserRepository userRepository, IMapper mapper,
        IValidator<UserForCreationDto> registrationValidator, IValidator<UserForLoginDto> loginValidator,
        IConfiguration config)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _registrationValidator = registrationValidator ?? throw new ArgumentNullException(nameof(registrationValidator));
        _loginValidator = loginValidator ?? throw new ArgumentNullException(nameof(loginValidator));
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> RegisterUser(UserForCreationDto userDto)
    {
        await _registrationValidator.ValidateAndThrowAsync(userDto);

        var user = _mapper.Map<User>(userDto);

        await _userRepository.AddUserAsync(user);

        await _userRepository.SaveChangesAsync();

        return CreatedAtAction("RegisterUser", _mapper.Map<UserDto>(user));
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> LoginUser(UserForLoginDto userDto)
    {
        await _loginValidator.ValidateAndThrowAsync(userDto);

        var userEntity = await _userRepository.GetUserAsync(userDto.Email);

        if (userEntity == default)
            return BadRequest("Invalid Credentials");

        if (userEntity.Password != userDto.Password)
            return BadRequest("Invalid Credentials!");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("UserId", userEntity.UserId.ToString()),
            new Claim("UserName", userEntity.UserName),
            new Claim("Email", userEntity.Email)
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
        var signIn = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(_config["JWT:ValidIssuer"],
            _config["JWT:ValidAudience"], claims,
            expires: DateTime.Now.AddDays(1), signingCredentials: signIn);

        var actualToken = new JwtSecurityTokenHandler().WriteToken(token);

        userEntity.Token = actualToken;

        await _userRepository.SaveChangesAsync();

        return Ok(_mapper.Map<UserDto>(userEntity));
    }
}