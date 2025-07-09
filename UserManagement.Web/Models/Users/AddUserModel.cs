﻿using System;

namespace UserManagement.Web.Models.Users;

public class AddUserModel
{
    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateOnly DateOfBirth {  get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}
