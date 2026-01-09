using System;
using System.Collections.Generic;

namespace services.Entity;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Requirements { get; set; } = null!;

    public string? Image { get; set; }

    public bool? Visible { get; set; }
}
