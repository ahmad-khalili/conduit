namespace Conduit.Core.Models;

public class UserForUpdateDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Bio { get; set; }
    public string? Image { get; set; }
}