using System;

namespace UserManagement.Web.Models.Users;

public class LogUserModel
{
    public int UserId { get; set; } = default!;
    public string Action { get; set; } = default!;
    public DateTime Timestamp { get; set; }
}
