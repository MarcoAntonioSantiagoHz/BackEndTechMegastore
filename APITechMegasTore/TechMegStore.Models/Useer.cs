using System;
using System.Collections.Generic;

namespace TechMegStore.Models;

public partial class Useer
{
    public int IdUser { get; set; }

    public string? CompleteName { get; set; }

    public string? Email { get; set; }

    public int? IdRole { get; set; }

    public string? Password { get; set; }

    public bool? Active { get; set; }

    public DateTime? DateRegistration { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }
}
