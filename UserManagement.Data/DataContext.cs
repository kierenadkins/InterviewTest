using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseInMemoryDatabase("UserManagement.Data.DataContext");

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<User>().HasData(new[]
        {
        new User { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", IsActive = true, DateOfBirth = new DateOnly(1990, 5, 21)},
        new User { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", IsActive = true, DateOfBirth = new DateOnly(1990, 5, 21) },
        new User { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", IsActive = false, DateOfBirth = new DateOnly(1990, 5, 21) },
        new User { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", IsActive = true, DateOfBirth = new DateOnly(1990, 5, 21) },
        new User { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", IsActive = true, DateOfBirth = new DateOnly(1990, 5, 21) },
        new User { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", IsActive = true, DateOfBirth = new DateOnly(1990, 5, 21) },
        new User { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", IsActive = false, DateOfBirth = new DateOnly(1990, 5, 21) },
        new User { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", IsActive = false, DateOfBirth = new DateOnly(1990, 5, 21) },
        new User { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", IsActive = false, DateOfBirth = new DateOnly(1990, 5, 21) },
        new User { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", IsActive = true, DateOfBirth = new DateOnly(1990, 5, 21) },
        new User { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", IsActive = true, DateOfBirth = new DateOnly(1990, 5, 21) },
    });

        model.Entity<Log>().HasData(new[]
        {
        new Log { Id = 1, UserId = 1, Action = "Created account", Timestamp = new DateTime(2024, 1, 10, 9, 0, 0) },
        new Log { Id = 2, UserId = 2, Action = "Logged in", Timestamp = new DateTime(2024, 1, 11, 10, 30, 0) },
        new Log { Id = 3, UserId = 3, Action = "Updated profile", Timestamp = new DateTime(2024, 2, 5, 14, 45, 0) },
        new Log { Id = 4, UserId = 4, Action = "Deactivated account", Timestamp = new DateTime(2024, 2, 20, 16, 0, 0) },
        new Log { Id = 5, UserId = 5, Action = "Changed password", Timestamp = new DateTime(2024, 3, 1, 8, 15, 0) },
        new Log { Id = 6, UserId = 6, Action = "Logged out", Timestamp = new DateTime(2024, 3, 15, 18, 0, 0) },
        new Log { Id = 7, UserId = 7, Action = "Viewed dashboard", Timestamp = new DateTime(2024, 4, 2, 11, 25, 0) },
        new Log { Id = 8, UserId = 8, Action = "Created post", Timestamp = new DateTime(2024, 4, 10, 13, 40, 0) },
        new Log { Id = 9, UserId = 9, Action = "Deleted comment", Timestamp = new DateTime(2024, 5, 4, 17, 5, 0) },
        new Log { Id = 10, UserId = 10, Action = "Updated settings", Timestamp = new DateTime(2024, 5, 20, 19, 0, 0) },
        new Log { Id = 11, UserId = 11, Action = "Reset password", Timestamp = new DateTime(2024, 6, 1, 7, 45, 0) },
    });
    }


    public DbSet<User>? Users { get; set; }

    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => base.Set<TEntity>();

    public void Create<TEntity>(TEntity entity) where TEntity : class
    {
        base.Add(entity);
        SaveChanges();
    }

    public new void Update<TEntity>(TEntity entity) where TEntity : class
    {
        base.Update(entity);
        SaveChanges();
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        base.Remove(entity);
        SaveChanges();
    }
}
