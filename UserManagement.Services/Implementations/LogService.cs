using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class LogService : ILogService
{
    private readonly IDataContext _dataAccess;
    public LogService(IDataContext dataAccess) => _dataAccess = dataAccess;

    public IEnumerable<Log> GetAll() => _dataAccess.GetAll<Log>();
    public void Save(Log log) => _dataAccess.Create(log);
    public List<Log> GetByUserId(int id)
    {
        var logs = _dataAccess.GetAll<Log>().ToList();
        return logs.Where(l => l.UserId == id).ToList();
    }
}
