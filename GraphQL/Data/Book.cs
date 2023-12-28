using System.ComponentModel.DataAnnotations;

namespace GraphQL.Data;

public class Book
{
    [ID]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public required string Title { get; set; }

    [ID(nameof(Author))]
    public int AuthorId { get; set; }

    public Author Author { get; set; } = default!;
}