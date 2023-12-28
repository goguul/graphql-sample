using System.ComponentModel.DataAnnotations;

namespace GraphQL.Data;

public class Author
{
    [ID]
    public int Id{get; set;}

    [Required]
    [StringLength(200)]
    public required string Name { get; set; }

    public ICollection<Book> Books {get; set;} = new List<Book>();
}