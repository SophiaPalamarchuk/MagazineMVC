using System;
using System.Collections.Generic;

namespace MagazineDomain.Model;

public partial class Reader
{
    public int ReaderId { get; set; }

    public string? ReaderName { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
