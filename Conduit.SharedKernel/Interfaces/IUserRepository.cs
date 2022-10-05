using Conduit.Core.Entities;

namespace Conduit.SharedKernel.Interfaces;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<User?> GetUserAsync(string userEmail);
    Task<User?> GetCurrentUserAsync(string token);
    Task SaveChangesAsync();
}