using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService 
{
    IEnumerable<User> GetAll();
    public User? GetById(int id);
    void Save (User user);
    void Delete(User user);
    void Update(User user);
}
