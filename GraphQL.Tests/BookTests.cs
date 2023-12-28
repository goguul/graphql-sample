using GraphQL.Books;
using GraphQL.Data;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;

namespace GraphQL.Tests;

public class BookTests
{
    [Fact]
    public async Task Book_Schema_Changed()
    {
        ISchema schema = await new ServiceCollection()
            .AddPooledDbContextFactory<LibraryDbContext>(
                options => options.UseInMemoryDatabase("Data Source=library.db"))
            .AddGraphQL()
            .RegisterDbContext<LibraryDbContext>(DbContextKind.Pooled)
            .AddMutationConventions()
            .AddQueryType()
            .AddMutationType()
            .AddSubscriptionType()
            .AddTypeExtension<BookQuery>()
            .AddTypeExtension<BookMutation>()
            .AddTypeExtension<BookSubscription>()
            .AddType<BookType>()
            .AddGlobalObjectIdentification()
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            .BuildSchemaAsync();

        schema.Print().MatchSnapshot();
    }
}