using System;
using System.Collections.Generic;

namespace services.Entity;

public partial class Request
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string ServiceName { get; set; } = null!;

    public string Query { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool? Status { get; set; }

    public string StatusText { get; set; } = null!;

    public bool UserNoti { get; set; }

    public bool? AdminNoti { get; set; }
}
