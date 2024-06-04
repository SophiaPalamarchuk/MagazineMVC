using System;
using System.Collections.Generic;

namespace MagazineDomain.Model;

public partial class Editor
{
    public int EditorId { get; set; }

    public string? EditorName { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
