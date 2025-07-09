using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService 
{
    IEnumerable<User> GetAll();
    void Save (User user);
    void Delete(User user);
}
