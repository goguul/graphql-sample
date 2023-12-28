using GraphQL.Authors;
using GraphQL.Books;
using GraphQL.Data;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;

namespace GraphQL.Tests;

public class AuthorTests
{
    [Fact]
    public async Task Author_Schema_Changed()
    {
        ISchema schema = await new ServiceCollection()
            .AddPooledDbContextFactory<LibraryDbContext>(
                options => options.UseInMemoryDatabase("Data Source=library.db"))
            .AddGraphQL()
            .RegisterDbContext<LibraryDbContext>(DbContextKind.Pooled)
            .AddMutationConventions()
            .AddQueryType()
            .AddMutationType()
            .AddTypeExtension<AuthorQuery>()
            .AddTypeExtension<AuthorMutation>()
            .AddTypeExtension<AuthorType>()
            .AddTypeExtension<AuthorInputType>()
            .AddGlobalObjectIdentification()
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            .BuildSchemaAsync();

        schema.Print().MatchSnapshot();
    }

    [Fact]
    public async Task Add_Author()
    {
        IRequestExecutor requestExecutor = await new ServiceCollection()
            .AddPooledDbContextFactory<LibraryDbContext>(
                options => options.UseInMemoryDatabase("Data Source=library.db"))
            .AddGraphQL()
            .RegisterDbContext<LibraryDbContext>(DbContextKind.Pooled)
            .AddMutationConventions()
            .AddQueryType()
            .AddMutationType()
            .AddTypeExtension<BookType>()
            .AddTypeExtension<AuthorQuery>()
            .AddTypeExtension<AuthorMutation>()
            .AddTypeExtension<AuthorType>()
            .AddTypeExtension<AuthorInputType>()
            .AddGlobalObjectIdentification()
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            .BuildRequestExecutorAsync();

        IExecutionResult executionResult = await requestExecutor.ExecuteAsync(@"
            mutation AddAuthor {
                addAuthor(
                    input: {
                        Name: ""Test Author""
                    }) {
                        Id,
                        Name
                    }
                )
            }
        ");

        executionResult.ToJson().MatchSnapshot();
    }
}