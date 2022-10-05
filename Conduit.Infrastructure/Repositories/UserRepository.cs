using Conduit.Core.Entities;
using Conduit.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<User?> GetUserAsync(string userEmail)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(userEmail));
    }

    public async Task<User?> GetCurrentUserAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Token.Equals(token));
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}