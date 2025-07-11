using System;
namespace UserManagement.Web.Models.Users;

public class ViewUserModel
{
    public int Id { get; set; }
    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public List<LogUserModel> Logs { get; set; } = new List<LogUserModel>();
}
