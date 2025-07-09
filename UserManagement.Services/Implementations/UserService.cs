using System;
using System.Collections.Generic;
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
}
