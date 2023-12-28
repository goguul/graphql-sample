using GraphQL.Data;

namespace GraphQL.Authors;

[Node]
[ExtendObjectType(typeof(Author))]
public class AuthorType
{
    [BindMember(nameof(Author.Books), Replace = true)]
    public IQueryable<Book> GetBooks(
        [Parent] Author author,
        LibraryDbContext libraryDbContext)
    {
        return libraryDbContext.Authors.Where(a => a.Id == author.Id)
            .SelectMany(a => a.Books);
    }

    [NodeResolver]
    public async Task<Author> GetAuthorById(
        [ID] int id,
        AuthorByIdDataLoader authorByIdDataLoader,
        CancellationToken cancellationToken)
    {
        return await authorByIdDataLoader.LoadAsync(id, cancellationToken);
    }
}