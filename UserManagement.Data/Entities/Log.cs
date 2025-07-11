using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models;

public class Log
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Action { get; set; } = default!;
    public DateTime Timestamp { get; set; }
}
