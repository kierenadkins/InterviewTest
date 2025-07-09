﻿using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();
    public void Save(User user) => _dataAccess.Create(user);
    public void Delete(User user) => _dataAccess.Delete(user);
    public void Update (User user) => _dataAccess.Update(user);
    public User? GetById(int id)
    {
        var users = _dataAccess.GetAll<User>().ToList();
        return users.FirstOrDefault(u => u.Id == id);
    }
}
