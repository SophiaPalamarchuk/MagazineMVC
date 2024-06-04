using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MagazineDomain.Model;

public partial class Article
{
    public int ArticleId { get; set; }
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]

    public string? Title { get; set; }

    public string? TextContent { get; set; }

    public int? AuthorId { get; set; }

    public int? EditorId { get; set; }

    public int? MagazineId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Editor? Editor { get; set; }

    public virtual Magazine? Magazine { get; set; }
}
