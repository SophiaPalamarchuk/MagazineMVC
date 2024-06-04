using System;
using System.Collections.Generic;

namespace MagazineDomain.Model;

public partial class Magazine
{
    public int MagazineId { get; set; }

    public string? MagazineName { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
