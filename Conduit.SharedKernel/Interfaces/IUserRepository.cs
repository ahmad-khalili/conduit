using Conduit.Core.Entities;

namespace Conduit.SharedKernel.Interfaces;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task SaveChangesAsync();
}