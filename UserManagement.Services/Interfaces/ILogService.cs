using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface ILogService
{
    IEnumerable<Log> GetAll();
    List<Log> GetByUserId(int id);
    void Save(Log log);
}
