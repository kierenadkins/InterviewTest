using System;

namespace UserManagement.Web.Models.Users;

public class LogUserModel
{
    public string Action { get; set; } = default!;
    public DateTime Timestamp { get; set; }
}
