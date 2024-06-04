using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MagazineDomain.Model;

public partial class Author
{
    [Key]
    public int AuthorId { get; set; }

    [Required(ErrorMessage  =  "Поле не повинно бути порожнім")]
         [Display(Name = "Ім'я")]
    public string? AuthorName { get; set; }
  [Display(Name = "Email")]
    public string? Email { get; set; }
    
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
