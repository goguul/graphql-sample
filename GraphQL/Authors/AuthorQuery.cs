using GraphQL.Data;

namespace GraphQL.Authors;

[ExtendObjectType(OperationTypeNames.Query)]
public class AuthorQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Author> GetAuthors(LibraryDbContext libraryDbContext)
        => libraryDbContext.Authors;

    public async Task<Author> GetAuthorById(
        [ID] int id,
        AuthorByIdDataLoader authorByIdDataLoader,
        CancellationToken cancellationToken)
        => await authorByIdDataLoader.LoadAsync(id, cancellationToken);
}