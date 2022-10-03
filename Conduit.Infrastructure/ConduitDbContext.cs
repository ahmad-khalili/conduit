﻿using Conduit.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure;

public class ConduitDbContext : DbContext
{
    public ConduitDbContext(DbContextOptions<ConduitDbContext> options): base(options)
    {
    }

    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>().HasIndex(a => a.Title).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}