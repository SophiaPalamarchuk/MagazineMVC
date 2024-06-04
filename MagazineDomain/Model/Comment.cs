using System;
using System.Collections.Generic;

namespace MagazineDomain.Model;

public partial class Comment
{
    public int CommentId { get; set; }

    public string? CommentText { get; set; }

    public int? ArticleId { get; set; }

    public int? CommentAuthorId { get; set; }

    public virtual Article? Article { get; set; }

    public virtual Reader? CommentAuthor { get; set; }
}
