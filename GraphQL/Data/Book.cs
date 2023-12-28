using System.ComponentModel.DataAnnotations;

namespace GraphQL.Data;

public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public required string Title { get; set; }

    public int AuthorId { get; set; }

    public Author Author { get; set; } = default!;
}