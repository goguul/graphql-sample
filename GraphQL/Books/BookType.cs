using GraphQL.Authors;
using GraphQL.Data;

namespace GraphQL.Books;

[ExtendObjectType(typeof(Book))]
public class BookType
{
    [BindMember(nameof(Book.Author), Replace = true)]
    public async Task<Author> GetAuthor(
        [Parent] Book book,
        AuthorByIdDataLoader authorByIdDataLoader,
        CancellationToken cancellationToken)
    {
        return await authorByIdDataLoader.LoadAsync(book.AuthorId, cancellationToken);
    }
}