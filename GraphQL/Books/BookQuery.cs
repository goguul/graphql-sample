using GraphQL.Data;

namespace GraphQL.Books;

[ExtendObjectType(OperationTypeNames.Query)]
public class BookQuery
{

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Book> GetBooks(LibraryDbContext libraryDbContext) 
        => libraryDbContext.Books;

    public async Task<Book> GetBookById(
        [ID] int id,
        BookByIdDataLoader bookByIdDataLoader,
        CancellationToken cancellationToken)
        => await bookByIdDataLoader.LoadAsync(id, cancellationToken);
}