using GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Authors;

public class AuthorByIdDataLoader : BatchDataLoader<int, Author>
{
    private readonly IDbContextFactory<LibraryDbContext> _dbContextFactory;

    public AuthorByIdDataLoader(
        IBatchScheduler batchScheduler,
        IDbContextFactory<LibraryDbContext> dbContextFactory,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, Author>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using var libraryDbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await libraryDbContext.Authors.Where(author => keys.Contains(author.Id))
            .ToDictionaryAsync(d => d.Id, cancellationToken);
    }
}