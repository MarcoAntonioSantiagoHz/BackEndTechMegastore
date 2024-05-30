using System;
using System.Collections.Generic;

namespace TechMegStore.Models;

public partial class DocumentNumber
{
    public int IdDocumentNumber { get; set; }

    public int LastNumber { get; set; }

    public DateTime? DateRegistration { get; set; }
}
