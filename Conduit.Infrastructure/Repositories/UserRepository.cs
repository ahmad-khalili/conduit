using Conduit.Core.Entities;
using Conduit.SharedKernel.Interfaces;

namespace Conduit.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ConduitDbContext _context;

    public UserRepository(ConduitDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddUserAsync(User user)
    {
        await _context.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}