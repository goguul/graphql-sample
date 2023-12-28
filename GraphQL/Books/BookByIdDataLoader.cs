using GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Books;

public class BookByIdDataLoader : BatchDataLoader<int, Book>
{
    private readonly IDbContextFactory<LibraryDbContext> _dbContextFactory;

    public BookByIdDataLoader(IBatchScheduler batchScheduler,
        IDbContextFactory<LibraryDbContext> dbContextFactory,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, Book>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using var libraryDbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await libraryDbContext.Books.Where(book => keys.Contains(book.Id))
            .ToDictionaryAsync(d => d.Id, cancellationToken: cancellationToken);
    }
}