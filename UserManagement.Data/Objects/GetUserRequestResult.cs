using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Domain.Objects;
public class GetUserRequestResult
{
    public User user { get; set; } = new User();
    public List<Log> logs {  get; set; } = new List<Log>();
}
